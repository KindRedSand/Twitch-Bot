using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Redstone;

namespace SnowModule.Items
{
    class Showel : Item
    {
        public override string Name => "Showel";

        public override int Price => 10;

        public override IEnumerable<string> PurchaseAliases => new string[] { "лопата", "showel" };

        public override string GetPurchaseString(int am)
        {
            switch (am)
            {
                case 0:
                    return $"";
                case 1:
                    return $"лопата";
                case 2:
                case 3:
                case 4:
                    return $"{am} лопаты";
                default:
                    return $"{am} лопат";
            }
        }

        public override string ToString()
        {
            switch (Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} лопата";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} лопаты";
                default:
                    return $"{Amouth} лопат";
            }
        }
    }
}
