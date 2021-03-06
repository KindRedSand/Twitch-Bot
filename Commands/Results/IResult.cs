﻿namespace TwitchBot.Commands
{
    public interface IResult
    {
        CommandError? Error { get; }
        string ErrorReason { get; }
        bool IsSuccess { get; }
    }
}
