using System;
using System.Threading.Tasks;
using TwitchBot.Commands.Builders;

namespace TwitchBot.Commands
{
    public abstract class ModuleBase : ModuleBase<ICommandContext> { }

    public abstract class ModuleBase<T> : IModuleBase
        where T : class, ICommandContext
    {
        public T Context { get; private set; }

        /// <summary>
        /// Sends a message to the source channel
        /// </summary>
        /// <param name="message">Contents of the message; o</param>
        protected virtual void Reply(string message)
        {
            Context.Channel.SendMessage(message);
        }

        protected virtual void BeforeExecute(CommandInfo command)
        {
        }

        protected virtual void AfterExecute(CommandInfo command)
        {
        }

        protected virtual void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
        }

        //IModuleBase
        void IModuleBase.SetContext(ICommandContext context)
        {
            var newValue = context as T;
            Context = newValue ?? throw new InvalidOperationException($"Invalid context type. Expected {typeof(T).Name}, got {context.GetType().Name}");
        }
        void IModuleBase.BeforeExecute(CommandInfo command) => BeforeExecute(command);
        void IModuleBase.AfterExecute(CommandInfo command) => AfterExecute(command);
        void IModuleBase.OnModuleBuilding(CommandService commandService, ModuleBuilder builder) => OnModuleBuilding(commandService, builder);
    }
}
