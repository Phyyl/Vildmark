using System;
using System.Collections.Generic;
using System.Linq;

namespace Vildmark.Helpers
{
    public static class EnumHelper
    {
        public static void ForEach<T>(Action<T> action) where T : Enum
        {
            foreach (var value in GetValues<T>())
            {
                action(value);
            }
        }

        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
