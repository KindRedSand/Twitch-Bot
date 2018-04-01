using Newtonsoft.Json;
using RazorwingGL.Framework.Logging;
using RazorwingGL.Framework.Online.API;
using RazorwingGL.Framework.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TwitchBot.API;
using TwitchBot.IRCClient;

namespace TwitchBot
{
    public partial class DialogWindow : Form
    {
        private static DialogWindow singleton;

        private static List<string> MessagesToAdd = new List<string> { };
        private static List<TabPage> TabsToAdd = new List<TabPage> { };
        private static List<TabPage> TabsToRemove = new List<TabPage> { };

        private static List<Action> Actions = new List<Action> { };

        private static OAuthToken Token;

        public DialogWindow()
        {
            InitializeComponent();

            messageTextBox.PreviewKeyDown += MessageTextBox_PreviewKeyDown;
            chatListBox.DoubleClick += ChatListBox_DoubleClick;


            singleton = this;

            channelTextBox.Text = BotEntry.Channel;

            UsernameTextBox.Text = BotEntry.Config.Get<string>(Config.BotSetting.Username);
            Token = OAuthToken.Parse(BotEntry.Config.Get<string>(Config.BotSetting.SuperSecretSettings));
            OATokenTextBox.Text = Token.AccessToken ?? string.Empty;
            BotEntry.NonCommandMessage += (IRCClient.ChannelMessageEventArgs msg) =>
            {
                Actions.Add(() =>
                {
                    foreach (string s in CommandsListBox.Items)
                    {
                        var arr = s.Split('⇒');
                        if (arr[0][0] != '!')
                        {
                            if (msg.Message.ToLower().Contains('!' + arr[0].ToLower()))
                            {
                                string output = arr[1].Replace("{username}", msg.Badge.DisplayName);
                                output = output.Replace("{channel}", msg.Channel);
                                output = output.Replace("{message}", msg.Message);
                                BotEntry.client.SendMessage(msg.Channel, output);
                                Logger.Log($"[GENERIC] [{arr[0]}] command executed for |{msg.Badge.DisplayName}|");
                            }
                        }
                        else
                        if (msg.Message.ToLower().Contains(arr[0].ToLower()))
                        {
                            string output = arr[1].Replace("{username}", msg.Badge.DisplayName);
                            output = output.Replace("{channel}", msg.Channel);
                            output = output.Replace("{message}", msg.Message);
                            BotEntry.client.SendMessage(msg.Channel, output);
                            Logger.Log($"[GENERIC] [{arr[0]}] command executed for |{msg.Badge.DisplayName}|");
                        }
                    }
                });
            };

            BotEntry.client.OnConnect += (s, e) =>
            {
                Actions.Add(() =>
                {
                    connectionStateLabel.Text = $"Connected";
                    connectButton.Text = $"Disconnect";
                    connectButton.Enabled = true;
                    this.Text = "Twitch bot API. State: [Connected]";
                });
            };

            BotEntry.client.ConnectionClosed += (s, e) =>
            {
                Actions.Add(() =>
                {
                    connectButton.Enabled = true;
                    connectButton.Text = $"Connect";
                    connectionStateLabel.Text = "Disconnected";
                    this.Text = "Twitch bot API. State: Disconnected";
                });

            };

            if (BotEntry.client.Connected)
            {
                connectionStateLabel.Text = $"Connected";
                connectButton.Text = $"Disconnect";
                connectButton.Enabled = true;
                this.Text = "Twitch bot API. State: [Connected]";
            }
        }

        private void ChatListBox_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText((string)chatListBox.SelectedItem);
        }

        private void MessageTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && messageTextBox.Text != string.Empty)
            {
                BotEntry.HandleMessage(BotEntry.client,new ChannelMessageEventArgs(BotEntry.Channel, "Console", messageTextBox.Text, new Badge { mod = true, sub = true, DisplayName = "Console" }));
                AddMessage();
            }
        }

        public static void AddTab(TabPage control)
        {
            TabsToAdd.Add(control);
        }

        public static void RemoveTab(TabPage control)
        {
            TabsToRemove.Add(control);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageTextBox.Text != string.Empty)
            {
                BotEntry.HandleMessage(BotEntry.client,new ChannelMessageEventArgs(BotEntry.Channel, "Console", messageTextBox.Text, new Badge { mod = true, sub = true, DisplayName = "Console" }));
                AddMessage();
            }
        }

        private void messageTextBox_TextChanged(object sender, EventArgs e)
        {

        }



        public static void AddMessage(string Text = "")
        {
            if (Text == string.Empty)
            {
                MessagesToAdd.Add(singleton.messageTextBox.Text);
                singleton.messageTextBox.Text = string.Empty;
            }else
            {
                MessagesToAdd.Add(Text);
            }
        }

        private void chatListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while(MessagesToAdd.Count > 0)
            {
                chatListBox.Items.Insert(0, MessagesToAdd[0]);
                MessagesToAdd.Remove(MessagesToAdd[0]);
            }
            while (TabsToAdd.Count > 0)
            {
                if (TabsToAdd[0] != null && !TabsToAdd[0].IsDisposed)
                    tabControl.Controls.Add(TabsToAdd[0]);
                TabsToAdd.Remove(TabsToAdd[0]);
            }
            while (TabsToRemove.Count > 0)
            {
                if (TabsToRemove[0] != null && !TabsToRemove[0].IsDisposed)
                    tabControl.Controls.Remove(TabsToRemove[0]);
                TabsToRemove.Remove(TabsToRemove[0]);
            }
            while (Actions.Count > 0)
            {
                Actions[0]?.Invoke();
                Actions.Remove(Actions[0]);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void joinButton_Click(object sender, EventArgs e)
        {

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (!BotEntry.client.Connected)
            {
                connectButton.Text = "Connecting...";
                BotEntry.client.Username = UsernameTextBox.Text;
                BotEntry.client.AuthToken = OATokenTextBox.Text;
                if (Token == null)
                    Token = new OAuthToken();
                Token.AccessToken = OATokenTextBox.Text;
                Token.RefreshToken = OATokenTextBox.Text;
                Token.ExpiresIn = 100;

                BotEntry.Config.Set(Config.BotSetting.Username, UsernameTextBox.Text);
                BotEntry.Config.Set(Config.BotSetting.SuperSecretSettings, Token.ToString());
                BotEntry.client.Connect();
                connectButton.Enabled = false;
            }else
            {
                BotEntry.client.Disconnect();
                connectButton.Enabled = false;
            }
        }

        //private static List<string> CommandsList = new List<string> { };

        private void commandsPlusButton_Click(object sender, EventArgs e)
        {
            string cmd = $"{commandsCommandTextBox.Text}⇒{commandFeedbackTextBox.Text}";
            //Commands.Add(cmd);
            CommandsListBox.Items.Add(cmd);
        }

        private void commandsReplaceButton_Click(object sender, EventArgs e)
        {
            string cmd = $"{commandsCommandTextBox.Text}⇒{commandFeedbackTextBox.Text}";
            int indx = CommandsListBox.SelectedIndex;
            if (indx == -1)
                return;
            CommandsListBox.Items.Remove(CommandsListBox.SelectedItem);

            CommandsListBox.Items.Insert(indx, cmd);
        }

        private void commandsMinusButton_Click(object sender, EventArgs e)
        {
            CommandsListBox.Items.Remove(CommandsListBox.SelectedItem);
        }

        private void CommandsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CommandsListBox.SelectedItem == null)
                return;

            var arr = ((string)CommandsListBox.SelectedItem).Split('⇒');
            commandsCommandTextBox.Text = arr[0];
            commandFeedbackTextBox.Text = arr[1];
        }

        public void Unload(DesktopStorage storage)
        {
            using (var file = new StreamWriter(storage.GetStream("Generic Commans.json", FileAccess.Write, FileMode.Truncate)))
            {
                file.Flush();
                file.Write(JsonConvert.SerializeObject(CommandsListBox.Items));
            }
        }

        public new void Load(DesktopStorage storage)
        {
            using (var stream = storage.GetStream("Generic Commans.json", FileAccess.ReadWrite, FileMode.OpenOrCreate))
            {
                using (var file = new StreamReader(stream))
                {
                    try
                    {
                        var l = new ListBox.ObjectCollection(CommandsListBox, JsonConvert.DeserializeObject<string[]>(file.ReadToEnd()));
                        CommandsListBox.Items.AddRange(l);
                    }
                    catch (System.ArgumentNullException e)
                    {
                        Logger.Log("Can't reand data from config for generic commands. Removing the file...");
                        storage.Delete("Generic Commans.json");
                    }

                }
            }

        }

        public static void UpdateChattersList(ChatData s)
        {
            Actions.Add(() => 
            {
                singleton.chattersListBox.Items.Clear();
                var range = s.chatters.viewers;
                range.AddRange(s.chatters.moderators);

                singleton.chattersListBox.Items.AddRange(new ListBox.ObjectCollection(singleton.chattersListBox, range.ToArray()));
            });
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void joinButton_Click_1(object sender, EventArgs e)
        {
            if (channelTextBox.Text != string.Empty)
            {
                if (BotEntry.Channel != "#kindredthebot")
                    BotEntry.client.PartChannel(BotEntry.Channel);
                if (BotEntry.Channel.Value[0] != '#')
                    BotEntry.Channel.Value = "#" + channelTextBox.Text.ToLower();
                channelTextBox.Text = string.Empty;
                BotEntry.client.JoinChannel(BotEntry.Channel);
            }
        }

        private void openConfigFolderButton_Click(object sender, EventArgs e)
        {
            BotEntry.Storage.OpenInNativeExplorer();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
