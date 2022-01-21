using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Helpers;

[assembly: MetadataUpdateHandler(typeof(HotReloadHelper))]

namespace Vildmark.Helpers
{
    public delegate void HotReloadEventHandler();

    public static class HotReloadHelper
    {
        public static event HotReloadEventHandler? OnHotReload;

        internal static class HotReloadManager
        {
            public static void UpdateApplication(Type[]? types)
            {
                OnHotReload?.Invoke();
            }
        }
    }
}
