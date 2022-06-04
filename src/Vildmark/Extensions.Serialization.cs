using Vildmark.Serialization;

namespace Vildmark;

public static partial class Extensions
{
    public static void WriteVersion(this IWriter writer, Version value)
    {
        if (writer.WriteIsDefault(value))
        {
            return;
        }

        writer.WriteValue(value.Major);
        writer.WriteValue(value.Minor);
        writer.WriteValue(value.Build);
        writer.WriteValue(value.Revision);
    }

    public static Version? ReadVersion(this IReader reader)
    {
        return reader.ReadIsDefault()
            ? default
            : new Version(reader.ReadValue<int>(), reader.ReadValue<int>(), reader.ReadValue<int>(), reader.ReadValue<int>());
    }
}
