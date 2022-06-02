namespace Vildmark.Logging;

public static class Logger
{
    public static LogType EnabledLogType { get; set; } = LogType.Fatal | LogType.Error | LogType.Exception | LogType.Warning;
    public static bool LogToStdout { get; set; } = true;
    public static bool LogToStderr { get; set; }
    public static string? LogFilePath { get; set; }

    public static void EnableLogType(LogType logType) => EnabledLogType |= logType;
    public static void DisableLogType(LogType logType) => EnabledLogType &= ~logType;

    public static void Trace(Exception ex) => Log(LogType.Trace, GetExceptionString(ex));
    public static void Debug(Exception ex) => Log(LogType.Debug, GetExceptionString(ex));
    public static void Info(Exception ex) => Log(LogType.Info, GetExceptionString(ex));
    public static void Message(Exception ex) => Log(LogType.Message, GetExceptionString(ex));
    public static void Warning(Exception ex) => Log(LogType.Warning, GetExceptionString(ex));
    public static void Exception(Exception ex) => Log(LogType.Exception, GetExceptionString(ex));
    public static void Error(Exception ex) => Log(LogType.Error, GetExceptionString(ex));
    public static void Fatal(Exception ex) => Log(LogType.Fatal, GetExceptionString(ex));

    public static void Trace(string message, Exception? ex = default) => Log(LogType.Trace, FormatMessage(message, ex));
    public static void Debug(string message, Exception? ex = default) => Log(LogType.Debug, FormatMessage(message, ex));
    public static void Info(string message, Exception? ex = default) => Log(LogType.Info, FormatMessage(message, ex));
    public static void Message(string message, Exception? ex = default) => Log(LogType.Message, FormatMessage(message, ex));
    public static void Warning(string message, Exception? ex = default) => Log(LogType.Warning, FormatMessage(message, ex));
    public static void Exception(string message, Exception ex) => Log(LogType.Exception, FormatMessage(message, ex));
    public static void Error(string message, Exception? ex = default) => Log(LogType.Error, FormatMessage(message, ex));
    public static void Fatal(string message, Exception? ex = default) => Log(LogType.Fatal, FormatMessage(message, ex));

    private static string GetExceptionString(Exception ex)
    {
        return EnabledLogType > LogType.Debug ? ex.ToString() : ex.Message;
    }

    private static void Log(LogType type, string message)
    {
        if (!EnabledLogType.HasFlag(type))
        {
            return;
        }

        string logLine = $"[{type}] {message}";

        if (LogToStdout)
        {
            Console.WriteLine(logLine);
        }

        if (LogToStderr)
        {
            Console.Error.WriteLine(logLine);
        }

        if (LogFilePath is string filePath)
        {
            try
            {
                File.AppendAllLines(filePath, new[] { logLine });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Logger] Failed to log to file: {filePath} ({ex.Message})");
            }
        }
    }

    private static string FormatMessage(string message, Exception? ex = default)
    {
        if (ex is null)
        {
            return message;
        }

        return string.Join(Environment.NewLine, message, GetExceptionString(ex));
    }
}
