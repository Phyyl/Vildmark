namespace Vildmark;

[Flags]
public enum LogType
{
    Fatal = 1,
    Error = 2,
    Exception = 4,
    Warning = 8,
    Message = 16,
    Info = 32,
    Debug = 64,
    Trace = 128
}
