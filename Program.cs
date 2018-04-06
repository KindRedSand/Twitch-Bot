using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RazorwingGL.Framework.Configuration;
using RazorwingGL.Framework.IO.Network;
using RazorwingGL.Framework.Logging;
using RazorwingGL.Framework.Platform.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TwitchBot.API;
using TwitchBot.API.Commands;
using TwitchBot.Commands;
using TwitchBot.Config;
using TwitchBot.IRCClient;
using TwitchBot.Redstone;

namespace TwitchBot
{
    public class BotEntry
    { 
        /// <summary>
        /// Information collected in twitch chat.
        /// Updating every 5 mins.
        /// </summary>
        public static SortedDictionary<string, TwitchChatters> ChannelsChatters = new SortedDictionary<string, TwitchChatters> { };

        /// <summary>
        /// IrcClient for connecting to chat
        /// </summary>
        internal static IrcClient client;

        /// <summary>
        /// Current chat
        /// </summary>
        public static Bindable<string> Channel;

        /// <summary>
        /// Bot configurations
        /// </summary>
        public static BotConfiguration Config;

        /// <summary>
        /// Bot storage in AppData
        /// </summary>
        public static WindowsStorage Storage;

        /// <summary>
        /// Users data, collected in chat
        /// </summary>
        public static SortedDictionary<string, Redstone.RedstoneData> RedstoneDust = new SortedDictionary<string, Redstone.RedstoneData> { };


        internal static List<Item> registeredItems = new List<Item> { };

        /// <summary>
        /// Registered module items
        /// </summary>
        public static List<Item> RegisteredItems => registeredItems;

        /// <summary>
        /// Second chatroom for dust actions
        /// </summary>
        public static Bindable<string> ChatBotChannel = new Bindable<string>($"#chatrooms:30773965:1a1bd140-55b3-4a63-815c-077b094ffba1");

        /// <summary>
        /// Engine related services
        /// </summary>
        private static IServiceProvider services;
        private static CommandService commands;

        /// <summary>
        /// Fired if no one command is executed
        /// </summary>
        public static Action<ChannelMessageEventArgs> NonCommandMessage;

        /// <summary>
        /// Fired evervy tick
        /// </summary>
        public static event Action OnTickActions;

        /// <summary>
        /// Fired when form is closing
        /// </summary>
        public static event Action OnExiting;

        /// <summary>
        /// Internal form
        /// </summary>
        private static DialogWindow Form;

        internal static DateTimeOffset GetUserListTimout = DateTimeOffset.Now;

        [STAThread]
        static void Main(string[] args)
        {



            //RC-Channel-ID 30773965
            //ChatRoom 1a1bd140-55b3-4a63-815c-077b094ffba1


            try
            {
                ///Load bot configuration
                Config = new BotConfiguration(Storage = new WindowsStorage("FoxBot"));
                Channel = Config.GetBindable<string>(BotSetting.Channel);
                ChatBotChannel = Config.GetBindable<string>(BotSetting.BotChatRoom);

                ///Set log folder
                Logger.Storage = Storage.GetStorageForDirectory("logs");

                ///Create if not exist
                Directory.CreateDirectory("./Modues");

                ///Create client instance
                client = new IrcClient()
                {
                    //Username = Nick,
                    //AuthToken = ServerPass,
                };

                ///Initialize command service
                commands = new CommandService(new CommandServiceConfig() { CaseSensitiveCommands = false, DefaultRunMode = RunMode.Sync, });

                ///Create form instance
                Form = new DialogWindow();

                ///------------------------------=================


                bool interrupt = false;
                ///Modules update thread. Please, use async func for long timed work in case not blocking other modules
                Thread updateThread = new Thread(() =>
                {
                    while (!interrupt)
                    {
                        ///update
                        if (DateTimeOffset.Now > GetUserListTimout)
                        {
                            var req = new JsonWebRequest<ChatData>($"https://tmi.twitch.tv/group/user/{Channel.Value.Replace("#", "")}/chatters");

                            req.Finished += () =>
                            {
                                DialogWindow.UpdateChattersList(req.ResponseObject);

                                if (BotEntry.ChannelsChatters.ContainsKey(BotEntry.Channel))
                                {
                                    BotEntry.ChannelsChatters[BotEntry.Channel] = req.ResponseObject.chatters;
                                }
                                else
                                {
                                    BotEntry.ChannelsChatters.Add(BotEntry.Channel, req.ResponseObject.chatters);
                                }
                            };

                            ///In case not block current thread
                            req.PerformAsync();

                            GetUserListTimout = GetUserListTimout.AddMinutes(5);
                        }

                        OnTickActions?.Invoke();

                    Thread.Sleep(50);
                    }
                });



                ///Load dust data
                using (var file = new StreamReader(Storage.GetStream($"Dust/{BotEntry.Channel}#RedstoneData.json", FileAccess.ReadWrite)))
                {
                    if (!file.EndOfStream)
                    {
                        BotEntry.RedstoneDust = JsonConvert.DeserializeObject<SortedDictionary<string, RedstoneData>>(file.ReadToEnd());
                    }
                    if (BotEntry.RedstoneDust == null)
                        BotEntry.RedstoneDust = new SortedDictionary<string, RedstoneData> { };
                }

                ///Load items metadata
                foreach (var data in RedstoneDust)
                {
                    data.Value.ReadJsonData();
                }

                ///Start update thread
                updateThread.Start();

                ///Load some configs in form
                Form.Load(Storage);

                services = new ServiceCollection()
                    .AddSingleton(client)
                    .AddSingleton(commands)
                    .AddSingleton(Form)
                    .BuildServiceProvider();

                client.ChannelMessage += HandleMessage;
                client.OnConnect += Client_OnConnect;

                commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);


                ///Try load all module in /Modules folder
                var dll = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Modues", "*.dll", SearchOption.TopDirectoryOnly);
                foreach (var it in dll)
                {
                    try
                    {
                        Logger.Log($"Loading {it} module...");
                        commands.AddModulesAsync(Assembly.LoadFile(it), services);
                    }
                    catch
                    {
                        continue;
                    }
                }


                ///Run form
                Application.Run(Form);

                ///Save any config changes inside form
                Logger.Log($"Unloading form data...");
                Form.Unload(Storage);

                ///Interrupt a thread
                interrupt = true;
                Thread.Sleep(1000);

                ///Unload all data and exit
                Logger.Log($"====================UNLOADING SERVICES=====================");
                OnExiting?.Invoke();
                client.Disconnect();
                ///Commands.CommandsService.Unload(Storage);
                Logger.Log($"=================UNLOADING SERVICES ENDED==================");

                ///Prepare item metadata to unloading
                foreach (var data in RedstoneDust)
                {
                    data.Value.PrepareJsonData();
                }

                ///Unload dust data
                using (var file = new StreamWriter(Storage.GetStream($"Dust/{BotEntry.Channel}#RedstoneData.json", FileAccess.ReadWrite, FileMode.Truncate)))
                {
                    file.Write(JsonConvert.SerializeObject(BotEntry.RedstoneDust));
                }

                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Logger.GetLogger(LoggingTarget.Stacktrace).Add($"FATAL ERROR: {e.Message}. SEE STACKTRACE FOR DETAIL");
                Logger.GetLogger(LoggingTarget.Stacktrace).Add(e.StackTrace);
                Thread.Sleep(50);

                foreach (var data in RedstoneDust)
                {
                    data.Value.PrepareJsonData();
                }

                using (var file = new StreamWriter(Storage.GetStream($"Dust/{BotEntry.Channel}#RedstoneData.json", FileAccess.ReadWrite, FileMode.Truncate)))
                {
                    file.Write(JsonConvert.SerializeObject(BotEntry.RedstoneDust));
                }
            }
        }

        internal static void HandleMessage(object sender, ChannelMessageEventArgs msg)
        {

            DialogWindow.AddMessage($"{msg.Channel} => {msg.Badge.DisplayName}: {msg.Message}");

            #region DustSection
            if (BotEntry.RedstoneDust.ContainsKey(msg.From))
            {
                if (msg.Badge.sub)
                {
                    if (BotEntry.RedstoneDust[msg.From].HasBoost)
                    { }
                    else
                    {
                        BotEntry.RedstoneDust[msg.From].HasBoost = true;
                    }
                }
                else
                {
                    if (BotEntry.RedstoneDust[msg.From].HasBoost && !BotEntry.Channel.Value.Contains("bot"))
                    {
                        BotEntry.RedstoneDust[msg.From].HasBoost = false;
                    }
                }
                BotEntry.RedstoneDust[msg.From].Chatter = true;
            }
            else
            {
                BotEntry.RedstoneDust.Add(msg.From, new Redstone.RedstoneData
                {
                    Dust = 0,
                    HasBoost = msg.Badge.sub,
                    Chatter = true
                });
            }

            if (RedstoneDust[msg.From].User == null || RedstoneDust[msg.From].User.Nick == null)
            {
                RedstoneDust[msg.From].User = new User(msg);
            }else
            {
                RedstoneDust[msg.From].User._mod = msg.Badge.mod;
                RedstoneDust[msg.From].User._sub = msg.Badge.sub;
            }
            #endregion

            int argPos = 0;
            /// Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(msg.HasCharPrefix('!', ref argPos)))
            {
                NonCommandMessage?.Invoke(msg);
                return;
            }

            /// Create a Command Context
            var context = new CommandContext(msg);

            /// If user want some help: read a Summary atribute for commands and reply it
            if (msg.HasStringPrefix("!help", ref argPos))
            {
                if(msg.Message.Substring(argPos).Length >= 1)
                {
                    var info = commands.Search(context, argPos + 1);
                    if (info.Commands.Count != 0)
                    {
                        {
                            client.SendMessage(msg.Channel, info.Commands[0].Command.Summary);
                            return;
                        }
                    }
                }else
                {
                    var text = new List<string> { };
                    foreach (var it in commands.Commands)
                    {
                        bool pass = false;
                        foreach(var a in it.Attributes)
                        {
                            
                            if (a is HideFromHelpAttribute)
                                pass = true;
                        }
                        if (pass)
                            continue;
                        if (it.Module.Group != null)
                        {
                            if (!text.Contains($"{it.Module.Group} {it.Name}"))
                                text.Add($"{it.Module.Group} {it.Name}");
                        }
                        else
                        {
                            if (!text.Contains($"{it.Name}"))
                                text.Add($"{it.Name}");
                        }

                    }
                    //if (text.Length > 2)
                    //text.Substring(0, text.Length - 2);
                    //else return;
                    var str = "";
                    foreach(var cmd in text)
                    {
                        str += $"{cmd}, ";
                    }
                    if (str.Length > 2)
                        str = str.Substring(0, str.Length - 2) + ".";
                    else return;


                    client.SendMessage(msg.Channel, str + " Используйте !help <команда> для дополнительных сведений");

                }

            }

            /// Execute the command. (result does not indicate a return value, 
            /// rather an object stating if the command executed successfully)
            var result = commands.ExecuteAsync(context, argPos, services);



            if (!result.Result.IsSuccess)
            {
                if (result.Result.ErrorReason.Contains("Комманда не найдена"))
                {
                    NonCommandMessage?.Invoke(msg);
                    return;
                }
                    
                client.SendMessage(msg.Channel, result.Result.ErrorReason);
                Logger.Log(result.Result.ErrorReason);
            }


        }

        /// <summary>
        /// Just do post connect channel joining and capabilities requesting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Client_OnConnect(object sender, EventArgs e)
        {
            client.SendRaw("CAP REQ :twitch.tv/tags");
            Thread.Sleep(500);
            client.SendRaw("CAP REQ :twitch.tv/commands");
            Thread.Sleep(500);
            client.JoinChannel(Channel);
            Thread.Sleep(500);
            client.JoinChannel($"#{Config.Get<string>(BotSetting.Username)}");
            Thread.Sleep(500);
            client.JoinChannel(ChatBotChannel);
        }

        /// <summary>
        /// Formate and localise time to string
        /// </summary>
        /// <param name="d">Days</param>
        /// <param name="h">Hours</param>
        /// <param name="m">Minutes</param>
        /// <param name="s">Seconds</param>
        /// <returns></returns>
        public static string FormateDate(int d, int h, int m, int s)
        {
            string output = "";
            if (d > 0)
            {
                if (d == 1)
                    output += $@"{d} день";
                else if (d == 2)
                    output += $@"{d} дня";
                else if (d == 3)
                    output += $@"{d} дня";
                else if (d == 4)
                    output += $@"{d} дня";
                else if (d == 5)
                    output += $@"{d} дней";
                else if (d == 6)
                    output += $@"{d} дней";
                else if (d == 7)
                    output += $@"{d} дней";
                else if (d == 8)
                    output += $@"{d} дней";
                else if (d == 9)
                    output += $@"{d} дней";
                else if (d == 10)
                    output += $@"{d} дней";
                else output += $@"{d} дней (Out of Range)";
                output += " ";
            }
            if (h > 0)
            {
                if (h == 1)
                    output += $@"{h} час";
                else if (h == 2)
                    output += $@"{h} часа";
                else if (h == 3)
                    output += $@"{h} часа";
                else if (h == 4)
                    output += $@"{h} часа";
                else if (h == 5)
                    output += $@"{h} часов";
                else if (h == 6)
                    output += $@"{h} часов";
                else if (h == 7)
                    output += $@"{h} часов";
                else if (h == 8)
                    output += $@"{h} часов";
                else if (h == 9)
                    output += $@"{h} часов";
                else if (h == 10)
                    output += $@"{h} часов";
                else if (h == 21)
                    output += $@"{h} час";
                else if (h == 22)
                    output += $@"{h} часа";
                else if (h == 23)
                    output += $@"{h} часа";
                else if (h == 24)
                    output += $@"{h} часа";
                else output += $@"{h} часов";
                output += " ";
            }
            if (m > 0)
            {
                if (m == 1)
                    output += $@"{m} минуту";
                else if (m == 2)
                    output += $@"{m} минуты";
                else if (m == 3)
                    output += $@"{m} минуты";
                else if (m == 4)
                    output += $@"{m} минуты";
                else if (m == 5)
                    output += $@"{m} минут";
                else if (m == 6)
                    output += $@"{m} минут";
                else if (m == 7)
                    output += $@"{m} минут";
                else if (m == 8)
                    output += $@"{m} минут";
                else if (m == 9)
                    output += $@"{m} минут";
                else if (m == 10)
                    output += $@"{m} минут";
                else if (m == 21)
                    output += $@"{m} минуту";
                else if (m == 22)
                    output += $@"{m} минуты";
                else if (m == 23)
                    output += $@"{m} минуты";
                else if (m == 24)
                    output += $@"{m} минуты";
                else if (m == 31)
                    output += $@"{m} минуту";
                else if (m == 32)
                    output += $@"{m} минуты";
                else if (m == 33)
                    output += $@"{m} минуты";
                else if (m == 34)
                    output += $@"{m} минуты";
                else if (m == 41)
                    output += $@"{m} минуту";
                else if (m == 42)
                    output += $@"{m} минуты";
                else if (m == 43)
                    output += $@"{m} минуты";
                else if (m == 44)
                    output += $@"{m} минуты";
                else if (m == 51)
                    output += $@"{m} минуту";
                else if (m == 52)
                    output += $@"{m} минуты";
                else if (m == 53)
                    output += $@"{m} минуты";
                else if (m == 54)
                    output += $@"{m} минуты";
                else output += $@"{m} минут";
                output += " ";
            }
            if (s > 0)
            {
                if (s == 1)
                    output += $@"{s} секунду";
                else if (s == 2)
                    output += $@"{s} секунды";
                else if (s == 3)
                    output += $@"{s} секунды";
                else if (s == 4)
                    output += $@"{s} секунды";
                else if (s == 5)
                    output += $@"{s} секунд";
                else if (s == 6)
                    output += $@"{s} секунд";
                else if (s == 7)
                    output += $@"{s} секунд";
                else if (s == 8)
                    output += $@"{s} секунд";
                else if (s == 9)
                    output += $@"{s} секунд";
                else if (s == 10)
                    output += $@"{s} секунд";
                else if (s == 21)
                    output += $@"{s} секунду";
                else if (s == 22)
                    output += $@"{s} секунды";
                else if (s == 23)
                    output += $@"{s} секунды";
                else if (s == 24)
                    output += $@"{s} секунды";
                else if (s == 31)
                    output += $@"{s} секунду";
                else if (s == 32)
                    output += $@"{s} секунды";
                else if (s == 33)
                    output += $@"{s} секунды";
                else if (s == 34)
                    output += $@"{s} секунды";
                else if (s == 41)
                    output += $@"{s} секунду";
                else if (s == 42)
                    output += $@"{s} секунды";
                else if (s == 43)
                    output += $@"{s} секунды";
                else if (s == 44)
                    output += $@"{s} секунды";
                else if (s == 51)
                    output += $@"{s} секунду";
                else if (s == 52)
                    output += $@"{s} секунды";
                else if (s == 53)
                    output += $@"{s} секунды";
                else if (s == 54)
                    output += $@"{s} секунды";
                else output += $@"{s} секунд";
                output += " ";
            }



            return output;
        }

        /// <summary>
        /// Formate and localise time through <see cref="TimeSpan"/> to string
        /// </summary>
        /// <param name="time"><see cref="TimeSpan"/> with time to display</param>
        /// <returns></returns>
        public static string FormateDate(TimeSpan time)
        {
            return FormateDate(time.Days, time.Hours, time.Minutes, time.Seconds);
        }

        /// <summary>
        /// Register item in to system. Used for inventory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterItem<T>() where T : Item
        {
            foreach (var it in RegisteredItems)
            {
                if (typeof(T) == it.GetType())
                {
                    return;
                }
            }
            RegisteredItems.Add(Activator.CreateInstance<T>());
            return;
        }
    }
}
