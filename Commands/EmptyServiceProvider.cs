using System;

namespace TwitchBot.Commands
{
    internal class EmptyServiceProvider2 : IServiceProvider
    {
        public static readonly EmptyServiceProvider2 Instance = new EmptyServiceProvider2();
        
        public object GetService(Type serviceType) => null;
    }
}
