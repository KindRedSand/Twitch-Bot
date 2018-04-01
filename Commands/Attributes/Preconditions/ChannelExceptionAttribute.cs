using RazorwingGL.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot;

namespace TwitchBot.Commands
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MainChannelExceptionAttribute : PreconditionAttribute
    {
        private string helpString = null;

        public MainChannelExceptionAttribute()
        {

        }

        public MainChannelExceptionAttribute(string Help)
        {
            helpString = Help;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {

                if (BotEntry.Channel.Value == context.Channel.ChannelID)
                {
                    return Task.FromResult(PreconditionResult.FromError($"Эта команда запрещена на этом канале чата. {helpString ?? string.Empty}"));
                }

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
