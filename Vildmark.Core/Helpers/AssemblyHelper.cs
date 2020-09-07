using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vildmark.Helpers
{
    public static class AssemblyHelper
    {
        private static bool loaded;

        public static void LoadAllReferencedAssemblies()
        {
            if (loaded)
            {
                return;
            }

            foreach (var assemblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                try
                {
                    Assembly.LoadFrom($"{assemblyName}.dll");
                }
                catch // miam
                {
                }
            }

            loaded = true;
        }
    }
}
