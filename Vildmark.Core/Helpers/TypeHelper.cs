using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vildmark.Helpers
{
    public static class TypeHelper
    {
        private static bool staticConstructorsRan;

        public static void RunAllStaticConstructors()
        {
            if (staticConstructorsRan)
            {
                return;
            }

            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()))
            {
                type.TypeInitializer?.Invoke(new object[0]);
            }

            staticConstructorsRan = true;
        }
    }
}
