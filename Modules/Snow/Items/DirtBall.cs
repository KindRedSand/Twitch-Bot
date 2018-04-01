using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Redstone;

namespace SnowModule.Items
{
    class DirtBall : Item
    {
        public override string Name => "Dirtball";

        public override int Price => -1;

        public override IEnumerable<string> PurchaseAliases => new string[] { "комок грязи", "dirt ball", "dirt", "грязь" };

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
    }
}
