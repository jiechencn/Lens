namespace Me.JieChen.Lens.Logging;

using System;
using System.Runtime.CompilerServices;


public interface ILogger<T> where T : class
{
    void LogError(
        string message,
        Exception exception = null,
        [CallerFilePath] string callerPath = "",
        [CallerLineNumber] int callerLine = 0,
        [CallerMemberName] string callerMethod = "");

    void LogWarning(
        string message,
        Exception exception = null,
        [CallerFilePath] string callerPath = "",
        [CallerLineNumber] int callerLine = 0,
        [CallerMemberName] string callerMethod = "");

    void LogInformation(
        string message,
        Exception exception = null,
        [CallerFilePath] string callerPath = "",
        [CallerLineNumber] int callerLine = 0,
        [CallerMemberName] string callerMethod = "");

    void LogDebug(
        string message,
        Exception exception = null,
        [CallerFilePath] string callerPath = "",
        [CallerLineNumber] int callerLine = 0,
        [CallerMemberName] string callerMethod = "");
}