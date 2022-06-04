using System.Text;

namespace Vildmark;

public static partial class Extensions
{
    public static string EscapeControlCharacters(this string str)
    {
        StringBuilder stringBuilder = new();

        foreach (var chr in str)
        {
            stringBuilder.Append(chr switch
            {
                '\0' => "\\0",
                '\a' => "\\a",
                '\b' => "\\b",
                '\f' => "\\f",
                '\n' => "\\n",
                '\r' => "\\r",
                '\t' => "\\t",
                '\v' => "\\v",
                _ => chr
            });
        }

        return stringBuilder.ToString();
    }

    public static string ResolveBackspaces(this string str)
    {
        StringBuilder builder = new();

        foreach (var chr in str)
        {
            switch (chr)
            {
                case '\b':
                    if (builder.Length > 0)
                    {
                        builder.Remove(builder.Length - 1, 1);
                    }
                    break;
                default:
                    builder.Append(chr);
                    break;
            }
        }

        return builder.ToString();
    }
}
