using System.Runtime.InteropServices;

namespace Vildmark.Helpers
{
    internal static class NativeLibraryHelper
    {
        public static IntPtr LoadNativeLibrary(string path)
        {
            string osFolder = OperatingSystem.IsLinux() ? "linux" : "windows";
            string loadPath = $@"libs\{osFolder}\{path}";

            return NativeLibrary.Load(loadPath);
        }
    }
}
