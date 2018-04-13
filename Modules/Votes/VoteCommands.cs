using RazorwingGL.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.API;
using TwitchBot.Commands;
using TwitchBot.Commands.Builders;
using TwitchBot;
using TwitchBot.IRCClient;

namespace TwitchBot.Modules.Votes
{
    [Group("vote"), Summary("Общая команда для голосований. !vote start что бы начать обычное голосование, !vote emoji что бы начать голосоване смайлами, либо !vote <число> во время активного обычного голосования для того что бы проголосовать")]
    public class VoteCommands : ModuleBase<CommandContext>
    {

        public static DateTimeOffset EndTime = DateTimeOffset.Now;

        public static bool started = false;
        public static string Condition = "";
        public static Dictionary<string, int> StandartVotes = new Dictionary<string, int> { };
        public static Dictionary<int, string> StandartVariants = new Dictionary<int, string> { };

        public static Dictionary<string, string> EmojiVotes = new Dictionary<string, string> { };
        public static Dictionary<string, string> EmojiVariants = new Dictionary<string, string> { };

        public static Channel StartChannel;

        public static bool SubOnly = false;

        const string subonlytext = "только для сабов";

        [Command("start"), Alias("начать"), Summary("Начать обычное голосование. Использование: !vote start <время в секундах> <Условие в \"кавычках\"> <Первый вариант в \"кавычках\"> <Второй вариант в \"кавычках\"> [Третий и прочие варианты каждый в \"кавычках\"]")]
        public async Task StartVote(int seconds, string cond, string var1, string var2, params string[] vars)
        {
            if (seconds < 20)
            {
                Reply($"@{Context.User.Nick} слишком мало времени: 20 > {seconds}");
            }
            if (seconds > 600)
            {
                Reply($"@{Context.User.Nick} слишком много времени: 600 > {seconds}");
            }

            if (started)
            {
                Reply($"@{Context.User.Nick} Нельзя начать голосование при активном голосовании!");
            }

            if (!(Context.User.HasMod || Context.User.HasSubscribe))
            {
                if (Context.User.Dust.Dust < 50)
                {
                    Reply($"@{Context.User.Nick} У вас недостаточно пыли для старта голосования. {Context.User.Dust.Dust} < {50}");
                    return;
                }
                else
                {
                    Context.User.Dust.Dust -= 50;
                }
            }

            SubOnly = false;

            if (cond.Length > 2)
            {
                if (cond[0] == '-' && cond[1] == 's')
                {
                    SubOnly = true;
                    cond = cond.Substring(2);
                }
            }

            Condition = cond;

            StandartVariants.Clear();
            StandartVotes.Clear();

            StandartVariants.Add(1, var1);
            StandartVariants.Add(2, var2);
            int i = 3;
            foreach (var it in vars)
            {
                StandartVariants.Add(i, it);
                i++;
            }
            string data = SubOnly ? subonlytext : "";

            string text = $"@{Context.User.Nick} Голосование {data} начато: {Condition}, Варианты: ";
            for (i = 1; i < StandartVariants.Count() + 1; i++)
            {
                text += $"{i}: {StandartVariants[i]} ";
            }

            EndTime = DateTimeOffset.Now.AddSeconds(seconds);

            Reply(text + ". Для того что бы проголосовать введите !v <Номер варианта>");

            started = true;

            EmodjiVote = false;

            StartChannel = Context.Channel;
        }

        [Command("emoji"), Summary("Начать голосование смайлами. Использование то же что и для обычного, только в вариантах первым указывается смайл, через пробел вариант: \"SeemsGood Выглядит неплохо\". Допускается пробел перед смайлом.")]
        public async Task StartEmoji(int seconds, string cond, string var1, string var2,params string[] vars)
        {
            if (seconds < 20)
            {
                Reply($"@{Context.User.Nick} слишком мало времени: 20 > {seconds}");
            }
            if (seconds > 600)
            {
                Reply($"@{Context.User.Nick} слишком много времени: 600 > {seconds}");
            }

            if (started)
            {
                Reply($"@{Context.User.Nick} Нельзя начать голосование при активном голосовании!");
            }

            if (!(Context.User.HasMod || Context.User.HasSubscribe))
            {
                if (Context.User.Dust.Dust < 50)
                {
                    Reply($"@{Context.User.Nick} У вас недостаточно пыли для старта голосования. {Context.User.Dust.Dust} < {50}");
                    return;
                }
                else
                {
                    Context.User.Dust.Dust -= 50;
                }
            }

            SubOnly = false;

            if (cond.Length > 2)
            {
                if (cond[0] == '-' && cond[1] == 's')
                {
                    SubOnly = true;
                    cond = cond.Substring(2);
                }
            }

            Condition = cond;

            EmojiVariants.Clear();
            EmojiVotes.Clear();

            var v = GetEmojiVariant(var1);
            if (v.Key != "")
            {
                EmojiVariants.Add(v.Key, v.Value);
            }
            v = GetEmojiVariant(var2);
            if (v.Key != "")
            {
                EmojiVariants.Add(v.Key, v.Value);
            }
            foreach (var it in vars)
            {
                v = GetEmojiVariant(it);
                if (v.Key != "")
                {
                    EmojiVariants.Add(v.Key, v.Value);
                }
            }
            if (EmojiVariants.Count < 2)
            {
                Reply($"@{Context.User.Nick} не удалось найти хотя-бы 2 валидных варианта. Голосование не начато");
                if (!(Context.User.HasMod || Context.User.HasSubscribe))
                {
                    Context.User.Dust.Dust += 50;
                }
                return;
            }
            string data = SubOnly ? subonlytext : "";


            string text = $"@{Context.User.Nick} Голосование смайлами {data} начато: {Condition}, Варианты: ";
            foreach (var it in EmojiVariants)
            {
                text += $"[ {it.Key} => {it.Value}]";
            }

            EndTime = DateTimeOffset.Now.AddSeconds(seconds);

            Reply(text + ". Для того что бы проголосовать просто киньте соответствующий смайл в чат");

            started = true;

            EmodjiVote = true;

            StartChannel = Context.Channel;
        }

        private KeyValuePair<string, string> GetEmojiVariant(string str)
        {
            var arr = str.Split(' ');
            int indx = 0;
            string emoji = "";
            for (; indx < arr.Count() && arr[indx] == ""; indx++) { }
            if (indx + 1 <= arr.Count())
            {
                emoji = arr[indx];
                string variant = arr[++indx];
                for (indx = indx+1; indx < arr.Count(); indx++)
                {
                    variant += $" {arr[indx]}";
                }
                return new KeyValuePair<string, string>(emoji, variant);
            }
            return new KeyValuePair<string, string>("", "");
        }






        public static bool EmodjiVote = false;

        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);


            BotEntry.OnTickActions += OnTick;
            BotEntry.NonCommandMessage += HandleEmoji;
        }

        public static void OnTick()
        {
            if (started && EndTime < DateTimeOffset.Now)
            {
                started = false;
                if (!EmodjiVote)
                {
                    string text = "Голосование окончено: ";
                    int allVotes = 0;
                    SortedDictionary<int, int> votesCount = new SortedDictionary<int, int> { };
                    foreach (var it in StandartVotes)
                    {
                        if (votesCount.ContainsKey(it.Value))
                        {
                            votesCount[it.Value]++;
                            allVotes++;
                        }
                        else
                        {
                            votesCount.Add(it.Value, 1);
                            allVotes++;
                        }
                    }

                    foreach (var it in votesCount)
                    {
                        var perc = new BindableFloat(((float)it.Value / (float)allVotes) * 100) { Precision = 0.01f, Value = (float)((float)it.Value / (float)allVotes) * 100 };
                        text += $"{StandartVariants[it.Key]}: {it.Value} <{perc.Value}> ";
                    }

                    if (text == "Голосование окончено: ")
                        StartChannel.SendMessage("Никто не проголосовал");
                    else
                    {
                        StartChannel.SendMessage(text);
                    }
                        
                }
                else
                {
                    string text = "Голосование окончено: ";
                    int allVotes = 0;
                    SortedDictionary<string, int> votesCount = new SortedDictionary<string, int> { };
                    foreach (var it in EmojiVotes)
                    {
                        if (votesCount.ContainsKey(it.Value))
                        {
                            votesCount[it.Value]++;
                            allVotes++;
                        }
                        else
                        {
                            votesCount.Add(it.Value, 1);
                            allVotes++;
                        }
                    }

                    foreach (var it in votesCount)
                    {
                        var perc = new BindableFloat(((float)it.Value / (float)allVotes) * 100) { Precision = 0.01f, Value = (float)((float)it.Value / (float)allVotes) * 100 };
                        try
                        {
                            text += $"[ {it.Key} {EmojiVariants[it.Key]}: {it.Value} <{perc.Value}>] ";
                        }
                        catch (Exception e)
                        {

                        }

                    }

                    if (text != "Голосование окончено: ")
                        StartChannel.SendMessage($"{text}");
                    else
                        StartChannel.SendMessage($"Никто не голосовал");
                }
            }
        }

        public static void HandleEmoji(ChannelMessageEventArgs msg)
        {
            if (!VoteCommands.started)
            {
                return;
                //ReplyAsync($"@{Context.User.Nick} голосование не начато!");
            }

            if (!VoteCommands.EmodjiVote)
            {
                return;
                //ReplyAsync($"@{Context.User.Nick} Во время голосования смайлами это недоступно");
            }
            if (SubOnly)
            {
                if (!msg.Badge.sub)
                    return;
            }

            var arr = msg.Message.Split(' ');

            if (VoteCommands.EmojiVariants.ContainsKey(arr[0]))
            {
                if (VoteCommands.EmojiVotes.ContainsKey(msg.From))
                {
                    VoteCommands.EmojiVotes[msg.From] = arr[0];
                }
                else
                {
                    VoteCommands.EmojiVotes.Add(msg.From, arr[0]);
                }
            }


        }
    }

    public class Vote : ModuleBase<CommandContext>
    {
        [Command("v"), Summary("Использует при обычном голосовании. Использование: !v <номер вариарта>")]
        public async Task PerformVote(int i)
        {
            if (!VoteCommands.started)
            {
                Reply($"@{Context.User.Nick} голосование не начато!");
            }

            if (VoteCommands.EmodjiVote)
            {
                return;
                //ReplyAsync($"@{Context.User.Nick} Во время голосования смайлами это недоступно");
            }

            if (i > VoteCommands.StandartVariants.Count)
            {
                return;
            }
            if (VoteCommands.SubOnly)
            {
                if (!Context.User.HasSubscribe)
                    return;
            }

            if (VoteCommands.StandartVotes.ContainsKey(Context.User.Username))
            {
                VoteCommands.StandartVotes[Context.User.Username] = i;
            }else
            {
                VoteCommands.StandartVotes.Add(Context.User.Username, i);
            }
        }
    }
}
