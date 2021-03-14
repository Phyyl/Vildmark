using System;

namespace Vildmark.Logging
{
    public record LogEntry(LogLevel Level, string Message, Exception Exception = null)
    {
        public DateTime DateTime { get; init; } = DateTime.Now;
    }
}
