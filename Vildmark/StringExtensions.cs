using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark
{
    public static class StringExtensions
    {
        public static string SafeSubString(this string str, int start)
        {
            return SafeSubString(str, start, str.Length - start);
        }

        public static string SafeSubString(this string str, int start, int length)
        {
            return str.Substring(Math.Min(start, str.Length - 1), Math.Min(length, str.Length - start));
        }
    }
}
