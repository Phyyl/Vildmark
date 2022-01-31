using System;
using System.Runtime.InteropServices;
using Vildmark.Helpers;

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

        public T[,]? Read2DValues<T>() where T : unmanaged
        {
            if (ReadIsDefault())
            {
                return default;
            }

            T[,] result = new T[ReadValue<int>(), ReadValue<int>()];

            ReadRaw(MemoryMarshal.CreateSpan(ref result[0,0], result.Length));

            return result;
        }

        public T[,,]? Read3DValues<T>() where T : unmanaged
        {
            if (ReadIsDefault())
            {
                return default;
            }

            T[,,] result = new T[ReadValue<int>(), ReadValue<int>(), ReadValue<int>()];

            ReadRaw(MemoryMarshal.CreateSpan(ref result[0, 0, 0], result.Length));

            return result;
        }

        public T? ReadObject<T>(bool includeType = false)
        {
            if (ReadIsDefault())
            {
                return default;
            }

            Type type = typeof(T);

            if (ReadString() is string typeName)
            {
                if (Type.GetType(typeName) is not Type foundType)
                {
                    Logger.Error($"Could not find type {typeName} for deserialization");
                    return default;
                }

                type = foundType;
            }

            if (TypeHelper.TryCreateIsntance(type, out object? result))
            {
                if (result is not T t)
                {
                    Logger.Error($"Could not cast {type.Name} to {typeof(T)} for deserialization");
                    return default;
                }

                return t;
            }
            else if (TypeHelper.TryCreateIsntance(type, out result, this))
            {
                if (result is not T t)
                {
                    Logger.Error($"Could not cast {type.Name} to {typeof(T)} for deserialization");
                    return default;
                }

                return t;
            }

            Logger.Error($"Could not instantiate type {type.Name} for deserialization");

            return default;
        }

        public T?[]? ReadObjects<T>(bool includeType = false)
        {
            if (ReadIsDefault())
            {
                return default;
            }

            T?[] result = new T[ReadValue<int>()];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = ReadObject<T>(includeType);
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
