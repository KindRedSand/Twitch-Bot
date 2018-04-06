using BlockBreaker.App.Main.Utils.MathUtils;
using RazorwingGL.Framework.IO.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Commands;
using TwitchBot;

namespace TwitchBot.Modules
{
    [LowLatency(5)]
    public class OptionalCommands : ModuleBase<CommandContext>
    {
        [Command("roll"), Summary("Выкинуть случайное число. !roll [максимальное число. По умолчанию = 6]")]
        public async Task Roll(int max = 6)
        {
            if (max > 1)
            {
                var i = RNG.Next(0, max + 1);
                Reply($"@{Context.User.Nick} => {i}");
            }
            else
            {
                Reply($"@{Context.User.Nick} => Число вне диапазона: 2 > {max}");
            }
        }

        [Command("uptime"), Alias("time", "время"), Summary("Выводит время которое стрим уже работает")]
        public async Task Uptime()
        {
            using (WebRequest req = new WebRequest($@"https://beta.decapi.me/twitch/uptime/{BotEntry.Channel.Value.Substring(1, BotEntry.Channel.Value.Length - 1)}"))
            {
                try
                {
                    req.Failed += (e) =>
                    {
                        Reply($"При выполнении запроса произошла ошибка: {e.Message}");
                    };
                    req.Perform();
                    if (req.Completed)
                    {
                        var vars = req.ResponseString.Split(' ');
                        bool offline = false;
                        if (vars.Length == 3)
                        {
                            if (vars[2] == "offline")
                            {
                                Reply($"@{Context.User.Nick}, Стрим оффлайн");
                                return;
                                offline = true;
                            }
                            else
                            {
                                offline = false;
                            }
                        }
                        if (!offline)
                        {
                            int d = 0, h = 0, m = 0, sec = 0;
                            var param = req.ResponseString.Split(',');
                            foreach (var it in param)
                            {
                                string s = it;
                                if (it[0] == ' ')
                                    s = s.Substring(1, s.Length - 1);
                                var pair = s.Split(' ');
                                if (pair[1] == "day" || pair[1] == "days")
                                {
                                    int.TryParse(pair[0], out d);
                                }
                                else if (pair[1] == "hour" || pair[1] == "hours")
                                {
                                    int.TryParse(pair[0], out h);
                                }
                                else if (pair[1] == "minute" || pair[1] == "minutes")
                                {
                                    int.TryParse(pair[0], out m);
                                }
                                else if (pair[1] == "second" || pair[1] == "seconds")
                                {
                                    int.TryParse(pair[0], out sec);
                                }
                            }

                            Reply($"@{Context.User.Nick}, Стрим работает уже {BotEntry.FormateDate(d, h, m, sec)}");
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    return;
                }

            }
        }

    }
}
