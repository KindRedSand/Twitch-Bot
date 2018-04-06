using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Commands
{
    public class LowLatencyAttribute : PreconditionAttribute
    {

        private int CSeconds;

        private DateTimeOffset CooldownEnd = DateTimeOffset.Now;

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (CooldownEnd > DateTimeOffset.Now)
                return Task.FromResult(PreconditionResult.FromError(""));

            CooldownEnd = DateTimeOffset.Now.AddSeconds(CSeconds);
            return Task.FromResult(PreconditionResult.FromSuccess());
        }

        public LowLatencyAttribute(int cooldown = 10)
        {
            CSeconds = cooldown;
        }

    }
}
