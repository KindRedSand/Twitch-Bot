using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Redstone;

namespace DustModule.Items
{
    class RedstoneTorch : Item
    {
        public override string Name => "RedTorch";

        public override int Price => 2;

        public override IEnumerable<string> PurchaseAliases => new string[] { "факел", "torch", "красный факел", "redstone torch" };

        public override string GetPurchaseString(int am)
        {
            switch (am)
            {
                case 0:
                    return $"";
                case 1:
                    return $"факел";
                case 2:
                case 3:
                case 4:
                    return $"{am} факела";
                default:
                    return $"{am} факелов";
            }
        }

        public override string ToString()
        { 
            switch(Amouth)
            {
                case 0:
                    return $"";
                case 1:
                    return $"{Amouth} факел";
                case 2:
                case 3:
                case 4:
                    return $"{Amouth} факела";
                default:
                    return $"{Amouth} факелов";
            }
        }
    }
}
