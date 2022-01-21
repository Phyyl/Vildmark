using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
