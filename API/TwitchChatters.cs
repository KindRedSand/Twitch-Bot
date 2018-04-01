using System.Collections.Generic;

namespace TwitchBot.API
{
    public class ChatData
    {
        public SortedDictionary<string, string> _links;
        public int chatter_count;
        public TwitchChatters chatters;
    }


    public class TwitchChatters
    {
        public List<string> moderators;
        public List<string> staff;
        public List<string> admins;
        public List<string> global_mods;
        public List<string> viewers;
    }
}