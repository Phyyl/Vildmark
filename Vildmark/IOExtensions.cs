namespace Vildmark;

public static class IOExtensions
{
    public static string[] ReadAllLines(this Stream stream)
    {
        StreamReader reader = new(stream);
        List<string> lines = new();

        while (reader.ReadLine() is string line)
        {
            lines.Add(line);
        }

        return lines.ToArray();
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
