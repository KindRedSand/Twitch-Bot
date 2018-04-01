using System;

namespace TwitchBot.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DontAutoLoadAttribute : Attribute
    {
    }
}
