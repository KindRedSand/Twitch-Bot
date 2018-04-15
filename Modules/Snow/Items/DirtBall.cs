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
    class DirtBall : Item, ITossableItem
    {
        public override string Name => "Dirtball";

        public override int Price => -1;

        public override IEnumerable<string> PurchaseAliases => new string[] { "комок грязи", "dirt ball", "dirt", "грязь" };

        public int Delay => 4;

        public bool OnlyTargetUser => false;

        public bool HasReflectionAction => false;

        public void BeforeShoot(ICommandContext Context)
        {
            Context.Channel.SendMessage("/me заряжает снегомёт комком грязи");
        }

        public void Shoot(ICommandContext Context, string target)
        {
            int rnd = RNG.Next(0, 6);
            if (rnd >= 2)
            {
                Context.Channel.SendMessage($"Бам, комок грязи от @{Context.User.Nick} прилетел прямо в лицо {target}");
            }
            else
            {
                Context.Channel.SendMessage($"Бам, комок грязи от @{Context.User.Nick} так и не достиг {target}");
            }
        }

        public override string GetPurchaseString(int am)
        {
            switch (am)
            {
                case 0:
                    return $"";
                case 1:
                    return $"комок грязи";
                case 2:
                case 3:
                case 4:
                    return $"{am} комка грязи";
                default:
                    return $"{am} комков грязи";
            }
        }

        public override string ToString()
        {
            switch (Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} комок грязи";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} комка грязи";
                default:
                    return $"{Amouth} комков грязи";
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
