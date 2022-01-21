using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vildmark.Logging;

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

            Assembly? assembly = Assembly.GetEntryAssembly();

            if (assembly is null)
            {
                Logger.Info($"{nameof(Assembly.GetEntryAssembly)} returned null");
                return;
            }

            foreach (var assemblyName in assembly.GetReferencedAssemblies())
            {
                try
                {
                    Assembly.LoadFrom($"{assemblyName.Name}.dll");
                }
                catch // miam
                {
                }
            }

            loaded = true;
        }

        public static IEnumerable<Assembly> GetAllLoadedUserAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company != "Microsoft Corporation");
        }
    }
}
