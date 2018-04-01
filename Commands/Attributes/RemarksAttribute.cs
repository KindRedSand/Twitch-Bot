using System;

namespace TwitchBot.Commands
{
    // Extension of the Cosmetic Summary, for Groups, Commands, and Parameters
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RemarksAttribute : Attribute
    {
        public string Text { get; }

        public RemarksAttribute(string text)
        {
            Text = text;
        }
    }
}
