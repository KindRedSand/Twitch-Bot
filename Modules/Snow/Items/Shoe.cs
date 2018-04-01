using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Redstone;

namespace SnowModule.Items
{
    class Shoe : Item
    {
        public override string Name => "shoe";

        public override int Price => 10;

        public override IEnumerable<string> PurchaseAliases => new string[] { "тапок", "тапки", "shoe", "shoes"};

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
    }
}
