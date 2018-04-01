using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot;
using TwitchBot.Redstone;

namespace TwitchBot.Commands.Readers
{
    public class ItemTypeReader<T> : TypeReader 
        where T : Item
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            foreach(var it in BotEntry.RegisteredItems)
            {
                foreach(var a in it.PurchaseAliases)
                {
                    if (a == input)
                    {
                        return Task.FromResult(TypeReaderResult.FromSuccess(it));
                    }
                }
            }

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ObjectNotFound, "Предмет не зарегестрирован!"));
        }
    }
}
