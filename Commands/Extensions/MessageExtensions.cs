using System;
using TwitchBot.API.Commands;

namespace TwitchBot.Commands
{
    public static class MessageExtensions
    {
        public static bool HasCharPrefix(this TwitchBot.IRCClient.ChannelMessageEventArgs msg, char c, ref int argPos)
        {
            var text = msg.Message;
            if (text.Length > 0 && text[0] == c)
            {
                argPos = 1;
                return true;
            }
            return false;
        }
        public static bool HasStringPrefix(this TwitchBot.IRCClient.ChannelMessageEventArgs msg, string str, ref int argPos, StringComparison comparisonType = StringComparison.Ordinal)
        {
            var text = msg.Message;
            if (text.StartsWith(str, comparisonType))
            {
                argPos = str.Length;
                return true;
            }
            return false;
        }
        public static bool HasMentionPrefix(this TwitchBot.IRCClient.ChannelMessageEventArgs msg, User user, ref int argPos)
        {
            var text = msg.Message;
            if (text.Length <= 3 || text[0] != '@') return false;

            int endPos = text.IndexOf(' ');
            if (endPos == -1) return false;
            if (text.Length < endPos) return false; //Must end in "> "

            string username = text.Substring(1, endPos);
            //if (!MentionUtils.TryParseUser(text.Substring(0, endPos + 1), out userId)) return false;
            if (username == user.Username)
            {
                argPos = endPos + 1;
                return true;
            }
            return false;

        }
    }
}