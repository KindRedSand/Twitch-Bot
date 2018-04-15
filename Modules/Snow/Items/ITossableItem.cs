using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.API.Commands;
using TwitchBot.Commands;

namespace SnowModule.Items
{
    public interface ITossableItem
    {

        void BeforeShoot(ICommandContext Context);
        int Delay { get; }
        void Shoot(ICommandContext Context, string target);
        bool OnlyTargetUser { get; }
        void Shoot(ICommandContext Context, User target);
        bool HasReflectionAction { get; }
        void OnReflection(ICommandContext context, User target);
    }
}
