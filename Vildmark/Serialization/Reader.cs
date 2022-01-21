using System.Runtime.InteropServices;

namespace Vildmark.Serialization
{
    public class Reader : IReader
    {
        public Stream BaseStream { get; }

        public Reader(Stream stream)
        {
            BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public unsafe T ReadValue<T>() where T : unmanaged
        {
            T result = new();

            ReadRaw(new Span<T>(&result, 1));

            return result;
        }

        public T[]? ReadValues<T>() where T : unmanaged
        {
            if (ReadIsDefault())
            {
                return default;
            }

            T[] result = new T[ReadValue<int>()];

            ReadRaw(result.AsSpan());

            return result;
        }

        public T? ReadObject<T>() where T : ISerializable, new()
        {
            if (ReadIsDefault())
            {
                return default;
            }

            T result = new();

            result.Deserialize(this);

            return result;
        }

        public T?[]? ReadObjects<T>() where T : ISerializable, new()
        {
            if (ReadIsDefault())
            {
                return default;
            }

            T?[] result = new T[ReadValue<int>()];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = ReadObject<T>();
            }

            return result;
        }

        public string? ReadString()
        {
            char[]? chars = ReadValues<char>();

            return chars != null ? new string(chars) : null;
        }

        public string?[]? ReadStrings()
        {
            if (ReadIsDefault())
            {
                return default;
            }

            string?[] result = new string[ReadValue<int>()];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = ReadString();
            }

            return result;
        }

        public bool ReadIsDefault()
        {
            return ReadValue<bool>();
        }

        private unsafe void ReadRaw<T>(Span<T> span) where T : unmanaged
        {
            byte[] buffer = new byte[span.Length * sizeof(T)];

            BaseStream.Read(buffer);
            MemoryMarshal.Cast<byte, T>(buffer).CopyTo(span);
        }
    }
}
