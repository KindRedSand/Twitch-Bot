using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Redstone;

namespace SnowModule.Items
{
    class Snowball : Item
    {
        public override string Name => "Snowball";

        public override int Price => -1;

        public override IEnumerable<string> PurchaseAliases => new string[] { "снежок", "snowball", "snow", "снег" };

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
    }

}
