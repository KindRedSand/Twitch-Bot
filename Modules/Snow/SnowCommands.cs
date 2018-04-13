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
        public async Task Toss(Item item, params string[] target)
        {
            if (item is ITossableItem)
            {
                var itm = Context.User.Dust.GetItem(item.GetType());
                
                if (itm.Amouth > 0)
                {
                    itm -= 1;
                }
                else
                {
                    Reply($"@{Context.User.Nick} У вас нет {itm.GetPurchaseString(1)} в инвентаре!");
                    return;
                }
                var tos = item as ITossableItem;

                var str = string.Join(" ", target);

                foreach(var it in Context.Channel.Moderators)
                {
                    if (str.ToLower().Contains(it.ToLower()))
                    {
                        Reply($"{str} защищён неведомой магией, и потомоу {item.GetPurchaseString(1)} отскочил прямо в лицо @{Context.User.Nick}");
                        return;
                    }
                }



                Task.Run(() =>
                {
                    tos.BeforeShoot(Context);
                    Thread.Sleep(tos.Delay * 1000);
                    tos.Shoot(Context, str);
                });
                return;
            }
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
