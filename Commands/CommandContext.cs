using TwitchBot.API;
using TwitchBot.API.Commands;
using TwitchBot.IRCClient;

namespace TwitchBot.Commands
{
    public class CommandContext : ICommandContext
    {

        public bool IsPrivate => Channel.ChannelID == User.Username;

        public IrcClient Client => TwitchBot.BotEntry.client;

        public Channel Channel { get; }

        public User User { get; }

        public string Message { get; }

        public CommandContext(ChannelMessageEventArgs msg)
        {
            Channel = new Channel() { ChannelID = msg.Channel };
            User = new User(msg);
            Message = msg.Message;
        }
    }
}
