using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Logging
{
    public record LogEntry(LogLevel Level, string Message, Exception Exception = null)
    {
        public DateTime DateTime { get; init; } = DateTime.Now;
    }
}
