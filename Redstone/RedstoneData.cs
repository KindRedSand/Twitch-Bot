using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TwitchBot.API.Commands;

namespace TwitchBot.Redstone
{
    public class RedstoneData : IEquatable<RedstoneData>, IDisposable
    {
        public int Dust = 0;
        public bool HasBoost = false;

        public User User;

        //public SortedDictionary<string, int> Items = new SortedDictionary<string, int> { };

        [JsonIgnore]
        private List<Item> items = new List<Item> { };


        private SortedDictionary<string, Tuple<int, string>> serialiseItemMetadata = new SortedDictionary<string, Tuple<int, string>> { };


        public SortedDictionary<string, Tuple<int, string>> SerialiseItemMetadata => serialiseItemMetadata;

        //public SortedDictionary<string, int> SerialiseItemData = new SortedDictionary<string, int> { };

        [JsonIgnore]
        public bool Chatter = false;

        [JsonIgnore]
        public List<Item> Items => items;

        public T GetItem<T>() where T : Item
        {
            foreach (var it in items)
            {
                if (typeof(T) == it.GetType())
                {
                    return (T)it;
                }
            }
            T item = Activator.CreateInstance<T>();
            items.Add(item);
            return item;
        }

        public Item GetItem(Type type)
        {
            if (type.BaseType != typeof(Item))
                return null;

            foreach (var it in items)
            {
                if (type == it.GetType())
                {
                    return it;
                }
            }
            Item item = (Item)Activator.CreateInstance(type);
            items.Add(item);
            return item;
        }


        public static RedstoneData operator +(RedstoneData data, int val)
        {
            data.Dust += val;
            return data;
        }
        public static RedstoneData operator -(RedstoneData data, int val)
        {
            data.Dust -= val;
            return data;
        }

        public bool Equals(RedstoneData other)
        {
            return Dust == other.Dust && HasBoost == other.HasBoost;
        }

        public override string ToString()
        {
            return $"{this.Dust} пыли";
        }

        public RedstoneData()
        {

        }

        //public void Migrate()
        //{
        //    foreach(var it in SerialiseItemData)
        //    {
        //        try
        //        {
        //            foreach (var type in BotEntry.RegisteredItems)
        //            {
        //                if (it.Key == type.GetType().ToString())
        //                {
        //                    GetItem(type.GetType()).Amouth = it.Value;

        //                    //GetItem(type.GetType()).Amouth = it.Value.Item1;
        //                    //GetItem(type.GetType()).RestoreMetadata(it.Value.Item2);
        //                    break;
        //                }
        //            }
        //            //Type.
        //            //GetItem(Type.GetType(it.Key, true, false)).Amouth = it.Value;
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}

        internal void ReadJsonData()
        {
            foreach (var it in serialiseItemMetadata)
            {
                try
                {
                    foreach(var type in BotEntry.RegisteredItems)
                    {
                        if (it.Key == type.GetType().ToString())
                        {
                            GetItem(type.GetType()).Amouth = it.Value.Item1;
                            GetItem(type.GetType()).RestoreMetadata(it.Value.Item2);
                            break;
                        }
                    }
                    //Type.
                    //GetItem(Type.GetType(it.Key, true, false)).Amouth = it.Value;
                }
                catch
                {

                }
            }
        }

        internal void PrepareJsonData()
        {
            Dispose(false);
        }

        ~RedstoneData()
        {
            //Dispose();
        }

        public void Dispose(bool isDisposing = false)
        {
            //throw new NotImplementedException();
            //serialiseItemData.Clear();
            SortedDictionary<string, Tuple<int, string>> MigrationData = new SortedDictionary<string, Tuple<int, string>> { };
            foreach(var it in serialiseItemMetadata)
            {
                bool contin = false;
                foreach(Item item in Items)
                {
                    if (item.GetType().ToString() == it.Key)
                    {
                        contin = true;
                        break;
                    }
                            //continue;
                }
                if (!contin)
                    MigrationData.Add(it.Key, it.Value);
            }
            serialiseItemMetadata.Clear();
            foreach(var it in MigrationData)
            {
                serialiseItemMetadata.Add(it.Key, it.Value);
            }


            foreach (var it in items)
            {
                serialiseItemMetadata.Add(it.GetType().ToString(), new Tuple<int, string>(it.Amouth, it.SaveMetadata()));
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

    public class DustComparer : IComparer<RedstoneData>
    {
        public int Compare(RedstoneData x, RedstoneData y)
        {
            if (x.Dust > y.Dust)
                return 1;
            else if (x.Dust == y.Dust)
                return 0;
            return -1;
        }
    }
}
