using System.Reflection.Metadata;

[assembly: MetadataUpdateHandler(typeof(Vildmark.HotReload.HotReloadHelper))]

namespace Vildmark.HotReload;

public static class HotReloadHelper
{
    public static event Action? OnHotReload;

#pragma warning disable IDE0060 // Remove unused parameter
    public static void UpdateApplication(Type[]? types)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        OnHotReload?.Invoke();
    }
}
