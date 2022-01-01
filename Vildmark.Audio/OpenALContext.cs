using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
