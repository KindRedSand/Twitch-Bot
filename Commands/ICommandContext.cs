using TwitchBot.API;
using TwitchBot.API.Commands;
using TwitchBot.IRCClient;

namespace TwitchBot.Commands
{
    public interface ICommandContext
    {
        IrcClient Client { get; }
        Channel Channel { get; }
        User User { get; }
        string Message { get; }
    }
}