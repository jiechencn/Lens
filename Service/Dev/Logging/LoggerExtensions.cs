namespace Me.JieChen.Lens.Logging;

using System;
using Microsoft.Extensions.Logging;

enum LoggerEventIds
{
    DebugEvent = 10000,
    InformationEvent = 20000,
    WarningEvent = 30000,
    ErrorEvent = 90000
}

public static class LoggerExtensions
{
    private const string Message = "[{0:Caller}][{1:Message}]";

    private static Action<ILogger, string, string, Exception> error = LoggerMessage.Define<string, string>(
        LogLevel.Error,
        (int)LoggerEventIds.ErrorEvent,
        Message);

    private static Action<ILogger, string, string, Exception> warning = LoggerMessage.Define<string, string>(
        LogLevel.Warning,
        (int)LoggerEventIds.WarningEvent,
        Message);

    private static Action<ILogger, string, string, Exception> information = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        (int)LoggerEventIds.InformationEvent,
        Message);

    private static Action<ILogger, string, string, Exception> debug = LoggerMessage.Define<string, string>(
        LogLevel.Debug,
        (int)LoggerEventIds.DebugEvent,
        Message);

    public static void LogError(
        this ILogger logger,
        string caller,
        string message,
        Exception exception = null)
    {
        error(logger, caller, message, exception);
    }

    public static void LogWarning(
        this ILogger logger,
        string caller,
        string message,
        Exception exception = null)
    {
        warning(logger, caller, message, exception);
    }

    public static void LogInformation(
        this ILogger logger,
        string caller,
        string message,
        Exception exception = null)
    {
        information(logger, caller, message, exception);
    }

    public static void LogDebug(
        this ILogger logger,
        string caller,
        string message,
        Exception exception = null)
    {
        debug(logger, caller, message, exception);
    }
}