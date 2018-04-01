using TwitchBot.Commands.Builders;

namespace TwitchBot.Commands
{
    internal interface IModuleBase
    {
        void SetContext(ICommandContext context);

        void BeforeExecute(CommandInfo command);
        
        void AfterExecute(CommandInfo command);

        void OnModuleBuilding(CommandService commandService, ModuleBuilder builder);
    }
}
