namespace TwitchBot.Commands
{
    public enum CommandError
    {
        //Search
        UnknownCommand = 1,

        //Parse
        ParseFailed,
        BadArgCount,

        //Parse (Type Reader)
        ObjectNotFound,
        MultipleMatches,

        //Preconditions
        UnmetPrecondition,

        //Execute
        Exception,

        //Runtime
        Unsuccessful
    }
}
