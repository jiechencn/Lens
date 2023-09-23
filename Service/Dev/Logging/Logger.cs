namespace Me.JieChen.Lens.Logging;

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using MsLogging = Microsoft.Extensions.Logging;

public class Logger<T> : ILogger<T> where T : class
{
    private readonly MsLogging.ILogger<T> logger;
    private const string Caller = "{0:CallerPath}({1}): {2:CallerMethod}";

    public Logger(MsLogging.ILogger<T> logger)
    {
        this.logger = logger;
    }

    public void LogDebug(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0, [CallerMemberName] string callerMethod = "")
    {
        var caller = string.Format(CultureInfo.InvariantCulture, Caller, callerPath, callerLine, callerMethod);
        logger.LogDebug(caller, message, exception);
    }

    public void LogInformation(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0, [CallerMemberName] string callerMethod = "")
    {
        var caller = string.Format(CultureInfo.InvariantCulture, Caller, callerPath, callerLine, callerMethod);
        logger.LogInformation(caller, message, exception);
    }

    public void LogWarning(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0, [CallerMemberName] string callerMethod = "")
    {
        var caller = string.Format(CultureInfo.InvariantCulture, Caller, callerPath, callerLine, callerMethod);
        logger.LogWarning(caller, message, exception);
    }

    public void LogError(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0, [CallerMemberName] string callerMethod = "")
    {
        var caller = string.Format(CultureInfo.InvariantCulture, Caller, callerPath, callerLine, callerMethod);
        logger.LogError(caller, message, exception);
    }
}