using System;
using System.Threading.Tasks;

namespace TwitchBot.Commands
{
    /// <summary>
    /// This attribute requires that the user invoking the command has a specified permission.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RequireUserPermissionAttribute : PreconditionAttribute
    {
        //public GuildPermission? GuildPermission { get; }
        public ChannelPermission ChannelPermission { get; }

        /// <summary>
        /// Require that the user invoking the command has a specified GuildPermission
        /// </summary>
        /// <remarks>This precondition will always fail if the command is being invoked in a private channel.</remarks>
        /// <param name="permission">The GuildPermission that the user must have. Multiple permissions can be specified by ORing the permissions together.</param>
        //public RequireUserPermissionAttribute(GuildPermission permission)
        //{
        //    //GuildPermission = permission;
        //    //ChannelPermission = null;
        //}
        /// <summary>
        /// Require that the user invoking the command has a specified ChannelPermission.
        /// </summary>
        /// <param name="permission">The ChannelPermission that the user must have. Multiple permissions can be specified by ORing the permissions together.</param>
        /// <example>
        /// <code language="c#">
        ///     [Command("permission")]
        ///     [RequireUserPermission(ChannelPermission.ReadMessageHistory | ChannelPermission.ReadMessages)]
        ///     public async Task HasPermission()
        ///     {
        ///         await ReplyAsync("You can read messages and the message history!");
        ///     }
        /// </code>
        /// </example>
        public RequireUserPermissionAttribute(ChannelPermission permission)
        {
            ChannelPermission = permission;
            //GuildPermission = null;
        }
        
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var User = context.User;

            //if (GuildPermission.HasValue)
            //{
            //    if (guildUser == null)
            //        return Task.FromResult(PreconditionResult.FromError("Command must be used in a guild channel"));                
            //    if (!guildUser.GuildPermissions.Has(GuildPermission.Value))
            //        return Task.FromResult(PreconditionResult.FromError($"User requires guild permission {GuildPermission.Value}"));
            //}


            switch (ChannelPermission)
            {
                case ChannelPermission.Subscriber:
                    if (!User.HasSubscribe)
                        return Task.FromResult(PreconditionResult.FromError($"User requires channel permission {ChannelPermission}"));
                    break;
                case ChannelPermission.Moderator:
                    if (!User.HasMod)
                        return Task.FromResult(PreconditionResult.FromError($"User requires channel permission {ChannelPermission}"));
                    break;
                case ChannelPermission.BothAny:
                    if (User.HasSubscribe || User.HasMod)
                    { }else
                        return Task.FromResult(PreconditionResult.FromError($"User requires channel permission {ChannelPermission}"));
                    break;
            }
 

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }

    public enum ChannelPermission
    {
        Subscriber,
        Moderator,
        BothAny,
    }
}
