using System.Reflection.Metadata;
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
