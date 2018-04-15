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
    class Shoe : Item, ITossableItem
    {
        public override string Name => "shoe";

        public override int Price => 10;

        public override IEnumerable<string> PurchaseAliases => new string[] { "тапок", "тапки", "shoe", "shoes"};

        public int Delay => 4;

        public bool OnlyTargetUser => false;

        public bool HasReflectionAction => false;

        public void BeforeShoot(ICommandContext Context)
        {
            Context.Channel.SendMessage("/me заряжает снегомёт тапком...");
        }

        public void Shoot(ICommandContext Context, string target)
        {
            int rnd = RNG.Next(0, 8);
            if (rnd >= 3 && rnd != 7)
            {
                Context.Channel.SendMessage($"Бам, отменный тапок от @{Context.User.Nick} прилетел прямо в лицо {target}");
            }
            else if (rnd == 2)
            {
                Context.Channel.SendMessage($"Бам, тапок от @{Context.User.Nick} пролетел по касательной прямо перед лицом {target}");
            }
            else if (rnd == 7)
            {
                if (Context.Channel.Chatters.Count != 0)
                {
                    int t = RNG.Next(0, Context.Channel.Chatters.Count);
                    Context.Channel.SendMessage($"Бам, тапок от @{Context.User.Nick} полетел... полетел.. мимо {target} и постиг лицо {Context.Channel.Chatters[t]} в расплох WutFace");
                }
            }
            else
            {
                Context.Channel.SendMessage($"Бам, тапок от @{Context.User.Nick} полетел... полетел.. мимо? Лицо {target} осталось невредимым PogChamp");
            }
        }

        public override string GetPurchaseString(int am)
        {
            switch (am)
            {
                case 0:
                    return $"";
                case 1:
                    return $"тапок";
                case 2:
                case 3:
                case 4:
                    return $"{am} тапка";
                default:
                    return $"{am} тапков";
            }
        }

        public override string ToString()
        {
            switch (Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} тапок";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} тапка";
                default:
                    return $"{Amouth} тапков";
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
