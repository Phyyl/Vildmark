using System;

namespace Vildmark.Logging
{
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
