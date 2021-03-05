using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vildmark.Serialization
{
    public class Writer : IWriter
    {
        public Stream BaseStream { get; }

        public Writer(Stream stream)
        {
            BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public unsafe void WriteValue<T>(T value) where T : unmanaged
        {
            WriteRaw(new Span<T>(&value, 1));
        }

        public void WriteValues<T>(T[] values) where T : unmanaged
        {
            if (WriteIsDefault(values))
            {
                return;
            }

            WriteValue(values.Length);
            WriteRaw(values.AsSpan());
        }

        public void WriteObject<T>(T value) where T : ISerializable, new()
        {
            if (WriteIsDefault(value))
            {
                return;
            }

            value.Serialize(this);
        }
        public void WriteObjects<T>(T[] values) where T : ISerializable, new()
        {
            if (WriteIsDefault(values))
            {
                return;
            }

            WriteValue(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                WriteObject(values[i]);
            }
        }

        public unsafe void WriteString(string value)
        {
            WriteValues(value?.ToCharArray());
        }

        public void WriteStrings(string[] values)
        {
            if (WriteIsDefault(values))
            {
                return;
            }

            WriteValue(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                WriteString(values[i]);
            }
        }

        public bool WriteIsDefault<T>(T value)
        {
            if (Equals(value, default))
            {
                WriteValue(true);
                return true;
            }

            WriteValue(false);

            return false;
        }

        private void WriteRaw<T>(Span<T> span) where T : unmanaged
        {
            BaseStream.Write(MemoryMarshal.Cast<T, byte>(span));
        }
    }
}
