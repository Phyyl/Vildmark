using System.Runtime.InteropServices;

namespace Vildmark.Helpers;

internal static class NativeLibraryHelper
{
    public static IntPtr LoadNativeLibrary(string name)
    {
        string osFolder = OperatingSystem.IsLinux() ? "linux-x64" : "win-x64";
        string loadPath = $@"runtimes\{osFolder}\native\{name}";

        return NativeLibrary.Load(loadPath);
    }
}
