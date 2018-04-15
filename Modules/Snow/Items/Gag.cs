using BlockBreaker.App.Main.Utils.MathUtils;
using System;
using System.Collections.Generic;
using System.Threading;
using TwitchBot.API.Commands;
using TwitchBot.Commands;
using TwitchBot.Redstone;

namespace SnowModule.Items
{
    public class Gag : Item, ITossableItem
    {
        public int Delay => 10;

        public bool OnlyTargetUser => true;

        public bool HasReflectionAction => true;

        public override string Name => "gag";

        public override int Price => 60;

        public override IEnumerable<string> PurchaseAliases => new string[] { "gag", "кляп" };

        public void BeforeShoot(ICommandContext Context)
        {
            Context.Channel.SendMessage("/me Заряжает мортиру...");
        }

        public override string GetPurchaseString(int amouth)
        {
            switch (Amouth)
            {
                case 0:
                    return $"кляп";
                case 1:
                    return $"кляп";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} кляпа";
                default:
                    return $"{Amouth} кляпов";
            }
        }

        public void OnReflection(ICommandContext context, User target)
        {
            context.Channel.SendMessage($"/timeout {context.User} 5");            
        }

        public void Shoot(ICommandContext Context, string target)
        {
            
        }

        public void Shoot(ICommandContext Context, User target)
        {
            if (RNG.Next(10) > 5)
            {
                Context.Channel.SendMessage($"Бам, кляп от @{Context.User.Nick} прилетел прямо в {target.Nick} 4Head");
                Thread.Sleep(10);
                Context.Channel.SendMessage($"/timeout {target.Username} 10");
            }
        }

        public override string ToString()
        {
            switch (Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} кляп";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} кляпа";
                default:
                    return $"{Amouth} кляпов";
            }
        }
    }
}
