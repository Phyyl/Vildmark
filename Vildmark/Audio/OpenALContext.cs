using OpenTK.Audio.OpenAL;
using Vildmark.Helpers;

namespace Vildmark.Audio
{
    internal static class OpenALContext
    {
        private static readonly ALDevice device;
        private static readonly ALContext context;

        static OpenALContext()
        {
            //string deviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);

            //foreach (var name in ALC.GetStringList(GetEnumerationStringList.DeviceSpecifier))
            //{
            //    if (name.Contains("OpenAL Soft"))
            //    {
            //        deviceName = name;
            //    }
            //}

            NativeLibraryHelper.LoadNativeLibrary("openal32");

            device = ALC.OpenDevice(null);
            context = ALC.CreateContext(device, (int[])null!);
            ALC.MakeContextCurrent(context);
        }

        public static ALSourceState GetSourceState(int sid)
        {
            ALC.MakeContextCurrent(context);
            return AL.GetSourceState(sid);
        }

        public static int GenBuffer() => AL.GenBuffer();
        public static int GenSource() => AL.GenSource();
    }
}
