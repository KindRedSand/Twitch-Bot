using BlockBreaker.App.Main.Utils.MathUtils;
using RazorwingGL.Framework.Extensions;
using SnowModule.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchBot.Commands;
using TwitchBot.Commands.Builders;
using TwitchBot.Redstone;

namespace TwitchBot.Modules.Snow
{
    public class SnowCommands : ModuleBase<CommandContext>
    {
        [Group("dig"), Alias("копать"), Summary("Выкопать различные вещи. Доступны: Снег, Земля"), MainChannelException("Копать только в комнате #bot SMOrc")]
        public class Dig : ModuleBase<CommandContext>
        {
            [Command("snow"), Alias("снег", "снежок", "снежков", "снега", "snowball"), Summary("Накопать снега")]
            public async Task Snow()
            {
                if (Context.User.Dust.GetItem<Showel>() > 0)
                {

                }
                else
                {
                    Reply($"@{Context.User.Nick} у вас нет лопат!");
                    return;
                }

                Context.User.Dust.GetItem<Showel>().Amouth -= 1;
                int rnds = RNG.Next(1, 12);
                Context.User.Dust.GetItem<Snowball>().Amouth += rnds;
                Reply($"@{Context.User.Nick} вы выкопали {rnds} снега, после чего лопата сломалась. Теперь у вас {Context.User.Dust.GetItem<Snowball>().ToString()}");
            }

            [Command("dirt"), Alias("земля", "dirtball"), Summary("Накопать земли")]
            public async Task Dirt()
            {
                if (Context.User.Dust.GetItem<Showel>() > 0)
                {

                }
                else
                {
                    Reply($"@{Context.User.Nick} у вас нет лопат!");
                    return;
                }

                Context.User.Dust.GetItem<Showel>().Amouth -= 1;
                int rnds = RNG.Next(1, 12);
                Context.User.Dust.GetItem<DirtBall>().Amouth += rnds;
                Reply($"@{Context.User.Nick} вы выкопали {rnds} земли, после чего лопата сломалась. Теперь у вас {Context.User.Dust.GetItem<DirtBall>().ToString()}");
            }
        }

        [Command("toss"), Alias("кинуть", "метнуть"), Summary("Кинуть предмет в кого-то")]
        public async Task Toss(Item item, string target)
        {
            if (item.Name.Contains("Dirtball", "shoe", "Snowball"))
            {
                switch(item.Name)
                {
                    case "Dirtball":
                        if (Context.User.Dust.GetItem<DirtBall>() > 0)
                            Task.Run(() =>
                            {
                                Context.User.Dust.GetItem<DirtBall>().Amouth -= 1;
                                Reply("/me заряжает снегомёт комком грязи");
                                Thread.Sleep(3000);
                                //SendMessage($"Бам, комок грязи от @{User.Nick} прилетел прямо в лицо {target}");
                                int rnd = RNG.Next(0, 6);
                                if (rnd >= 2)
                                {
                                    Reply($"Бам, комок грязи от @{Context.User.Nick} прилетел прямо в лицо {target}");
                                }
                                else
                                {
                                    Reply($"Бам, комок грязи от @{Context.User.Nick} так и не достиг {target}");
                                }

                            });
                        return;
                        break;
                    case "shoe":
                        if (Context.User.Dust.GetItem<Shoe>() > 0)
                            Task.Run(() =>
                            {
                                Context.User.Dust.GetItem<DirtBall>().Amouth -= 1;
                                Reply("/me заряжает снегомёт тапком...");
                                Thread.Sleep(3000);

                                int rnd = RNG.Next(0, 6);
                                if (rnd >= 3)
                                {
                                    Reply($"Бам, отменный тапок от @{Context.User.Nick} прилетел прямо в лицо {target}");
                                }
                                else if (rnd == 2)
                                {
                                    Reply($"Бам, тапок от @{Context.User.Nick} пролетел по касательной прямо перед лицом {target}");
                                }
                                else
                                {
                                    Reply($"Бам, тапок от @{Context.User.Nick} полетел... полетел.. мимо? Лицо {target} осталось невредимым PogChamp");
                                }

                            });
                        break;
                    case "Snowball":
                        if (Context.User.Dust.GetItem<Snowball>() > 0)
                            Task.Run(() =>
                            {
                                Context.User.Dust.GetItem<Snowball>().Amouth -= 1;
                                Reply("/me заряжает снегомёт снежком");
                                Thread.Sleep(3000);
                                //SendMessage($"Бам, снежок @{User.Nick} прилетел прямо в лицо {target}");
                                int rnd = RNG.Next(0, 6);
                                if (rnd >= 3)
                                {
                                    Reply($"Бам, снежок @{Context.User.Nick} прилетел прямо в лицо {target}");
                                }
                                else if (rnd == 2)
                                {
                                    Reply($"Бам, снежок @{Context.User.Nick} прилетел прямо в лицо {target} заморозив территорию вокруг. Шанс уворота снижен вдвое! 4Head");
                                }
                                else
                                {
                                    Reply($"Бам, снежок @{Context.User.Nick} так и не достиг {target} ...");
                                }
                            });
                        break;

                }
            }else
            {
                Reply($"@{Context.User.Nick} нельзя просто взять и запихнуть {item.GetPurchaseString(1)} в снегомёт");
            }
        }


        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            TwitchBot.BotEntry.RegisterItem<Showel>();
            TwitchBot.BotEntry.RegisterItem<Snowball>();
            TwitchBot.BotEntry.RegisterItem<DirtBall>();
            TwitchBot.BotEntry.RegisterItem<Shoe>();
        }
    }
}
