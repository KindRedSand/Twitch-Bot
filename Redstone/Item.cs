using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Redstone
{
    public abstract class Item : IEquatable<Item>
    {
        public abstract string Name { get; }
        private int amouth = 0;
        public int Amouth { get => amouth; set => amouth = value; }
        public abstract int Price { get; }

        public abstract IEnumerable<string> PurchaseAliases { get; }

        public static Item operator +(Item data, int val)
        {
            data.amouth += val;
            return data;
        }
        public static Item operator -(Item data, int val)
        {
            data.amouth -= val;
            return data;
        }

        public static bool operator >(Item it, int val)
        {
            return it.Amouth > val;
        }

        public static bool operator <(Item it, int val)
        {
            return it.Amouth < val;
        }

        public static bool operator >=(Item it, int val)
        {
            return it.Amouth >= val;
        }

        public static bool operator <=(Item it, int val)
        {
            return it.Amouth <= val;
        }

        public static bool operator ==(Item it, int val)
        {
            return it.Amouth == val;
        }

        public static bool operator !=(Item it, int val)
        {
            return it.Amouth != val;
        }

        

        public abstract string GetPurchaseString(int amouth);

        public bool Equals(Item other)
        {
            return amouth == other.amouth && Name == other.Name;
        }

        public virtual string SaveMetadata() => string.Empty;

        public virtual void RestoreMetadata(string s) { }

        public new abstract string ToString();
        /*
        {
            return $"{this.amouth} {Name}";
        }
        */
    }
}
