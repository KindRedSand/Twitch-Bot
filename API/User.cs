using System;
using TwitchBot.Redstone;
using TwitchBot.IRCClient;
using TwitchBot.Commands;
using Newtonsoft.Json;

namespace TwitchBot.API.Commands
{
    public class User : IDisposable
    {
        private string _username;
        public string Username => _username;
        private string _nick;
        public string Nick => _nick;
        internal bool _sub;
        public bool HasSubscribe => _sub;
        internal bool _mod;
        public bool HasMod => _mod;
        [JsonIgnore]
        public RedstoneData Dust => BotEntry.RedstoneDust[_username];
        public User(ChannelMessageEventArgs msg)
        {
            _username = msg.From;
            _nick = msg.Badge.DisplayName;
            _sub = msg.Badge.sub;
            _mod = msg.Badge.mod;
        }

        public User()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
