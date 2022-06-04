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
}
