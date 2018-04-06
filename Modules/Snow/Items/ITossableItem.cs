using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Commands;

namespace SnowModule.Items
{
    public interface ITossableItem
    {
        void BeforeShoot(ICommandContext Context);
        int Delay { get; }
        void Shoot(ICommandContext Context, string target); 
    }
}
