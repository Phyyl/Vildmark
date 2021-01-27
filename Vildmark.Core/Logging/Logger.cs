using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Resources;

namespace Vildmark.Logging
{
    [Register(typeof(ILogger), -1)]
    [Register(typeof(Logger), -1)]
    public class Logger : ILogger
    {
        public LogLevel Level { get; set; }

        public event LogEventHandler OnLog;

        public void Log(LogEntry entry)
        {
            if (Level < entry.Level)
            {
                return;
            }

            Task.Run(delegate
            {
                OnLog?.Invoke(this, entry);
            });

            Console.WriteLine($"[{entry.Level}][{entry.DateTime}] {entry.Message}");
        }
    }
}
