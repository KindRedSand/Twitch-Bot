using System;

namespace TwitchBot.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GroupAttribute : Attribute
    {
        public string Prefix { get; }

        public GroupAttribute()
        {
            Prefix = null;
        }
        public GroupAttribute(string prefix)
        {
            Prefix = prefix;
        }
    }
}
