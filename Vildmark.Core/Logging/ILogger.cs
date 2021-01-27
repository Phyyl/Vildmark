using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Logging
{
    public delegate void LogEventHandler(ILogger logger, LogEntry entry);

    public interface ILogger
    {
        event LogEventHandler OnLog;

        LogLevel Level { get; set; }

        void Trace(string message) => Log(new LogEntry(LogLevel.Trace, message));
        void Debug(string message) => Log(new LogEntry(LogLevel.Debug, message));
        void Message(string message) => Log(new LogEntry(LogLevel.Message, message));
        void Info(string message) => Log(new LogEntry(LogLevel.Info, message));
        void Warning(string message) => Log(new LogEntry(LogLevel.Warning, message));
        void Exception(Exception ex) => Log(new LogEntry(LogLevel.Exception, ex.Message, ex));
        void Error(string message) => Log(new LogEntry(LogLevel.Error, message));
        void Fatal(string message) => Log(new LogEntry(LogLevel.Fatal, message));

        void Log(LogEntry entry);
    }
}
