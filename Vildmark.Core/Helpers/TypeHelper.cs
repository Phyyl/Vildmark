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

            foreach (var type in AssemblyHelper.GetAllLoadedUserAssemblies().SelectMany(a => a.GetTypes()))
            {
                type.TypeInitializer?.Invoke(null, null);
            }

            staticConstructorsRan = true;
        }
    }
}
