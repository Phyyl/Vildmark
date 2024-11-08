using System.Runtime.InteropServices;
using System.Text;
using Vildmark.Helpers;
using Vildmark.Logging;

namespace Vildmark.Serialization;

public class Reader(Stream stream) : IReader
{
    public Stream BaseStream { get; } = stream ?? throw new ArgumentNullException(nameof(stream));
    public Encoding Encoding { get; init; } = Encoding.UTF8;

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
            if (TypeHelper.FindType(typeName) is not Type foundType)
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

            if (t is IDeserializable deserializable)
            {
                deserializable.Deserialize(this);
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
        byte[]? data = ReadValues<byte>();

        return data is null ? null : Encoding.GetString(data);
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
