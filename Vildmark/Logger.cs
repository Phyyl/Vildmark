using System;
using System.Threading.Tasks;
using Vildmark.Resources;

namespace Vildmark.Logging
{
    public delegate void LogEventHandler(LogEntry entry);

    public record LogEntry(LogLevel Level, string Message, Exception? Exception = null)
    {
        public DateTime DateTime { get; init; } = DateTime.Now;
    }

    public static class Logger
    {
        public static LogLevel Level { get; set; }

        public static event LogEventHandler? OnLog;

        public static void Trace(string message) => Log(new LogEntry(LogLevel.Trace, message));
        public static void Debug(string message) => Log(new LogEntry(LogLevel.Debug, message));
        public static void Message(string message) => Log(new LogEntry(LogLevel.Message, message));
        public static void Info(string message) => Log(new LogEntry(LogLevel.Info, message));
        public static void Warning(string message) => Log(new LogEntry(LogLevel.Warning, message));
        public static void Exception(Exception ex) => Log(new LogEntry(LogLevel.Exception, ex.Message, ex));
        public static void Error(string message) => Log(new LogEntry(LogLevel.Error, message));
        public static void Fatal(string message) => Log(new LogEntry(LogLevel.Fatal, message));

        public static void Log(LogEntry entry)
        {
            if (Level < entry.Level)
            {
                return;
            }

            Task.Run(delegate
            {
                OnLog?.Invoke(entry);
            });

            Console.WriteLine($"[{entry.Level}][{entry.DateTime}] {entry.Message}");
        }
    }

    [Flags]
    public enum LogLevel : byte
    {
        None = 0,
        Fatal = 1,
        Error = 2,
        Exception = 4,
        Warning = 8,
        Info = 16,
        Message = 32,
        Debug = 64,
        Trace = 128,
        All = Trace | Debug | Message | Info | Warning | Exception | Error | Fatal
    }
}
