using BlockBreaker.App.Main.Utils.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Commands;
using TwitchBot;

namespace TwitchBot.Modules
{
    [LowLatency]
    public class HoleCommands : ModuleBase<CommandContext>
    {
        public static string[] night = new string[] { "@{username} прощается со всеми. Пожелаем ему наконец проснуться!",
        "@{username} прощается со всеми. Пусть спит спокойно...",
        "@{username} прощается со всеми. И пусть сегодня во сне его не постигнут гачи",
        "@{username} прощается со всеми. Надеемся тебя не будут будить дудками...",
        "@{username} прощается со всеми. Только смотри не проспи LUL"};

        public static string[] morning = new string[] { "@{username} прощается со всеми. Надеемся он успеет вовремя",
        "@{username} прощается со всеми. Только не завались обратно в кравать LUL",
        "@{username} прощается со всеми. Не забудь позавтракать!",
        "@{username} прощается со всеми. Пожелаем ему удачного дня"};

        public static string[] day = new string[] { "@{username} прощается со всеми. Пожелаем ему удачного дня",
        "@{username} прощается со всеми. Не забудь покушац"};

        public static string[] evening = new string[] { "@{username} прощается со всеми. Пожелаем ему хорошо провести вечер",
        "@{username} прощается со всеми. Если ты свалил смотреть телик вместо стрима - ты ばか :(",
        "@{username} прощается со всеми. не забудь поставить будильник",};

        public static string[] mentions = new string[] { "CoolCat Бобра тебе, @{username}",
        "@{username} заявился на вечеринку, налейте ему фирменного от заведения DrinkPurple",
        "Эй, да это же @{username} EleGiggle",
        "@{username} has joined server",
        "twitchRaid @{username} десантировался на съёмочную площадку. Что ты вобще тут забыл?",
        "А ну поприветствуй всех живо, @{username} SMOrc",
        "Эй, @{username} объявился. Все делайте вид что заняты Kappa .",
        "Древнее зло настигает нас! NotLikeThis @{username} уже тут!",
        "Только не ставь гачи WutFace , @{username}",
        "Народ, @{username} привёз с собой доритос. Налетай GivePLZ DoritosChip TakeNRG",
        "Восславь солнце! \\[T]/ @{username}",};

        public static SortedDictionary<string, DateTimeOffset> ByeMentioned = new SortedDictionary<string, DateTimeOffset>();


        [Command("artel"), Alias("артел")]
        public async Task Artel(params string[] s)
        {
            Reply("Не паникуем. Артел свалил в Австралию, ему не до кубов.");
        }

        [Command("hole"), Alias("яма")]
        public async Task Hole(params string[] s)
        {
            Reply("Praise the Hole!!!!!1!11!1");
        }

        [Command("praise"), Alias("восславь", "солнце", "sun")]
        public async Task Sun(params string[] s)
        {
            Reply($"Восславь солнце! \\[T]/ @{Context.User.Nick}");
        }

        [Command("ret"), Summary("Заявить о себе после ухода.")]
        public async Task PerformReturn(params string[] s)
        {
            if (ByeMentioned.ContainsKey(Context.User.Username))
            {
                var date = ByeMentioned[Context.User.Username];
                ByeMentioned.Remove(Context.User.Username);

                Reply($"@{Context.User.Nick} вернулся спустя {BotEntry.FormateDate(DateTimeOffset.Now.LocalDateTime - date.LocalDateTime)}");
                return;
            }
        }

        [Command("bb"), Alias("бб", "пока", "byeall", "bye"), Summary("Сказать всем что ты покидаешь чатик. Используйте !ret что бы заявить о своём возвращении")]
        public async Task PerformBye(params string[] s)
        {
            if (ByeMentioned.ContainsKey(Context.User.Username))
            {

            }
            else
            {
                int quarter = (DateTime.Now.ToLocalTime().Hour - 2) / 6;
                switch (quarter)
                {
                    case 1:
                        string str = morning[RNG.Next(morning.Length)].Replace("{username}", Context.User.Nick);
                        Reply(str);
                        //client.SendMessage(Channel, $"@{msg.Badge.DisplayName} прощается со всеми. Пожелаем ему наконец проснуться!");

                        break;
                    case 2:
                        str = day[RNG.Next(day.Length)].Replace("{username}", Context.User.Nick);
                        Reply(str);
                        //client.SendMessage(Channel, $"@{msg.Badge.DisplayName} прощается со всеми. Пожелаем ему удачного дня!");
                        //byeMentioned.Add(msg.From);
                        break;
                    case 3:
                        str = evening[RNG.Next(evening.Length)].Replace("{username}", Context.User.Nick);
                        Reply(str);
                        //client.SendMessage(Channel, $"@{msg.Badge.DisplayName} прощается со всеми. Пожелаем ему хорошо провести вечер");
                        //byeMentioned.Add(msg.From);
                        break;
                    case 4:
                        str = night[RNG.Next(night.Length)].Replace("{username}", Context.User.Nick);
                        Reply(str);
                        //client.SendMessage(Channel, $"@{msg.Badge.DisplayName} прощается со всеми. Пожелаем ему добрых снов!");
                        //byeMentioned.Add(msg.From);
                        break;
                    default:
                        str = night[RNG.Next(night.Length)].Replace("{username}", Context.User.Nick);
                        Reply(str);
                        //client.SendMessage(Channel, $"@{msg.Badge.DisplayName} прощается со всеми. Пожелаем ему добрых снов!");
                        //byeMentioned.Add(msg.From);
                        break;
                }
                ByeMentioned.Add(Context.User.Username, DateTimeOffset.Now.LocalDateTime);
            }
        }

    }
}
