using System;

namespace TwitchBot.Commands
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class RemainderAttribute : Attribute
    {
    }
}
