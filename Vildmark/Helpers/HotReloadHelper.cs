using System.Reflection.Metadata;
using Vildmark.Helpers;

[assembly: MetadataUpdateHandler(typeof(HotReloadHelper))]

namespace Vildmark.Helpers;

public delegate void HotReloadEventHandler();

public static class HotReloadHelper
{
    public static event HotReloadEventHandler? OnHotReload;

    internal static class HotReloadManager
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static void UpdateApplication(Type[]? types)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            OnHotReload?.Invoke();
        }
    }
}
