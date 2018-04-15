using BlockBreaker.App.Main.Utils.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Commands;
using TwitchBot.Redstone;
using TwitchBot.API.Commands;

namespace SnowModule.Items
{
    class Snowball : Item, ITossableItem
    {
        public override string Name => "Snowball";

        public override int Price => -1;

        public override IEnumerable<string> PurchaseAliases => new string[] { "снежок", "snowball", "snow", "снег" };

        public int Delay => 4;

        public bool OnlyTargetUser => false;

        public bool HasReflectionAction => false;

        public void BeforeShoot(ICommandContext Context)
        {
            Context.Channel.SendMessage("/me заряжает снегомёт снежком");
        }

        public void Shoot(ICommandContext Context, string target)
        {
            int rnd = RNG.Next(0, 6);
            if (rnd >= 3)
            {
                Context.Channel.SendMessage($"Бам, снежок @{Context.User.Nick} прилетел прямо в лицо {target}");
            }
            else if (rnd == 2)
            {
                Context.Channel.SendMessage($"Бам, снежок @{Context.User.Nick} прилетел прямо в лицо {target} заморозив территорию вокруг. Шанс уворота снижен вдвое! 4Head");
            }
            else
            {
                Context.Channel.SendMessage($"Бам, снежок @{Context.User.Nick} так и не достиг {target} ...");
            }
        }

        public override string GetPurchaseString(int am)
        {
            switch (am)
            {
                case 0:
                    return $"";
                case 1:
                    return $"снежок";
                case 2:
                case 3:
                case 4:
                    return $"{am} снежка";
                default:
                    return $"{am} снежков";
            }
        }

        public override string ToString()
        {
            switch (Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} снежок";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} снежка";
                default:
                    return $"{Amouth} снежков";
            }
        }

        public void Shoot(ICommandContext Context, User target)
        {
            Shoot(Context, target.Nick);
        }

        public void OnReflection(ICommandContext context, User target)
        {
            
        }
    }

}
