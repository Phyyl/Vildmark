using System.Runtime.InteropServices;

namespace Vildmark.Helpers;

internal static class NativeLibraryHelper
{
    public static IntPtr LoadNativeLibrary(string windowsName, string linuxName, string macOSName)
    {
        string? path = null;

        if (OperatingSystem.IsWindows())
        {
            path = $"runtimes\\win-x64\\native\\{windowsName}";
        }
        else if (OperatingSystem.IsLinux())
        {
            path = $"runtimes/linux-x64/native/{linuxName}";
        }
        else if (OperatingSystem.IsMacOS())
        {
            path = $"runtimes/osx-x64/native/{macOSName}";
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        return NativeLibrary.Load(path);
    }
}
