using RazorwingGL.Framework.Logging;
using System;

namespace TwitchBot.Commands
{
    public class CommandServiceConfig
    {
        /// <summary> Gets or sets the default RunMode commands should have, if one is not specified on the Command attribute or builder. </summary>
        public RunMode DefaultRunMode { get; set; } = RunMode.Sync;

        public char SeparatorChar { get; set; } = ' ';
        
        /// <summary> Determines whether commands should be case-sensitive. </summary>
        public bool CaseSensitiveCommands { get; set; } = false;

        /// <summary> Gets or sets the minimum log level severity that will be sent to the Log event. </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Verbose;

        /// <summary> Determines whether RunMode.Sync commands should push exceptions up to the caller. </summary>
        public bool ThrowOnError { get; set; } = true;

        /// <summary> Determines whether extra parameters should be ignored. </summary>
        public bool IgnoreExtraArgs { get; set; } = false;
    }
}
