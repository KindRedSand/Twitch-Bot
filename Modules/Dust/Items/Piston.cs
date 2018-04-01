using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Redstone;

namespace DustModule.Items
{
    class Piston : Item
    {
        public override string Name => "Piston";

        public override int Price => 4;

        public override IEnumerable<string> PurchaseAliases => new string[] { "поршень", "piston"};

        public override string GetPurchaseString(int am)
        {
            switch (am)
            {
                case 0:
                    return $"";
                case 1:
                    return $"поршень";
                case 2:
                case 3:
                case 4:
                    return $"{am} поршня";
                default:
                    return $"{am} поршней";
            }
        }

        public override string ToString()
        {
            switch (Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} поршень";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} поршня";
                default:
                    return $"{Amouth} поршней";
            }
        }
    }
}
