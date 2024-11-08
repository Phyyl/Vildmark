namespace Vildmark;

public static partial class Extensions
{
    public static string[] ReadAllLines(this Stream stream)
    {
        StreamReader reader = new(stream);
        List<string> lines = [];

        while (reader.ReadLine() is string line)
        {
            lines.Add(line);
        }

        return [.. lines];
    }

    public static byte[] ReadAllBytes(this Stream stream)
    {
        using MemoryStream ms = new();
        stream.CopyTo(ms);
        return ms.ToArray();
    }

    public static string ReadAllText(this Stream stream)
    {
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}
