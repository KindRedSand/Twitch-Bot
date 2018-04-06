using System.Collections.Generic;
using TwitchBot;

namespace TwitchBot.API
{
    public class Channel
    {
        public string ChannelID = "null";
        public string ChannelName = "null";

        public List<string> Chatters
        {
            get
            {
                var list = new List<string> { };
                ///Use later...
                //if (ChannelID[0] != '#' || !BotEntry.ChannelsChatters.ContainsKey(ChannelID))
                //    return list;

                //list.AddRange(BotEntry.ChannelsChatters[ChannelID].viewers);
                //list.AddRange(BotEntry.ChannelsChatters[ChannelID].moderators);

                if(BotEntry.ChannelsChatters.ContainsKey(BotEntry.Channel))
                {
                    list.AddRange(BotEntry.ChannelsChatters[BotEntry.Channel].viewers);
                }

                return list;
            }
        }

        public List<string> Moderators
        {
            get
            {
                var list = new List<string> { };
                if (ChannelID[0] != '#' || !BotEntry.ChannelsChatters.ContainsKey(ChannelID))
                    return list;

                list.AddRange(BotEntry.ChannelsChatters[ChannelID].moderators);

                return list;
            }
        }

        public List<string> Subscribers
        {
            get
            {
                var list = new List<string> { };
                if (ChannelID[0] != '#' || !BotEntry.ChannelsChatters.ContainsKey(ChannelID))
                    return list;

                foreach(var s in Chatters)
                {
                    if (BotEntry.RedstoneDust.ContainsKey(s.ToLower()))
                    {
                        if (BotEntry.RedstoneDust[s.ToLower()].HasBoost)
                        {
                            list.Add(s);
                        }
                    }
                }

                return list;
            }
        }

        public void SendMessage(string Message)
        {
            BotEntry.client.SendMessage(ChannelID, Message);
        }
    }
}