using System.Runtime.InteropServices;
using System.Text;

namespace Vildmark.Serialization;

public class Writer(Stream stream) : IWriter
{
    public Stream BaseStream { get; } = stream ?? throw new ArgumentNullException(nameof(stream));
    public Encoding Encoding { get; init; } = Encoding.UTF8;
    public TypeNameSerializationOptions TypeNameSerializationOptions { get; init; } = TypeNameSerializationOptions.Name;

    public unsafe void WriteValue<T>(T value) where T : unmanaged
    {
        WriteValues(new Span<T>(&value, 1));
    }

    public void WriteValues<T>(T[]? values) where T : unmanaged
    {
        if (WriteIsDefault(values))
        {
            return;
        }

        if (values is not null)
        {
            WriteValue(values.Length);
            WriteValues(values.AsSpan());
        }
    }

    public void WriteValues<T>(T[,]? values) where T : unmanaged
    {
        if (WriteIsDefault(values))
        {
            return;
        }

        if (values is not null)
        {
            WriteValue(values.GetLength(0));
            WriteValue(values.GetLength(1));
            WriteValues(MemoryMarshal.CreateSpan(ref values[0, 0], values.Length));
        }
    }

    public void WriteValues<T>(T[,,]? values) where T : unmanaged
    {
        if (WriteIsDefault(values))
        {
            return;
        }

        if (values is not null)
        {
            WriteValue(values.GetLength(0));
            WriteValue(values.GetLength(1));
            WriteValue(values.GetLength(2));
            WriteValues(MemoryMarshal.CreateSpan(ref values[0, 0, 0], values.Length));
        }
    }

    public void WriteValues<T>(Span<T> span) where T : unmanaged
    {
        BaseStream.Write(MemoryMarshal.Cast<T, byte>(span));
    }

    public void WriteObject<T>(T? value, bool includeType = false) where T : ISerializable
    {
        if (WriteIsDefault(value))
        {
            return;
        }

        Type type = value!.GetType();

        string? typeName = TypeNameSerializationOptions switch
        {
            TypeNameSerializationOptions.FullName => type.FullName,
            TypeNameSerializationOptions.AssemblyQualifiedName => type.AssemblyQualifiedName,
            _ => type.Name
        };

        WriteString(includeType ? typeName : null);

        value!.Serialize(this);
    }

    public void WriteObjects<T>(T?[]? values, bool includeType = false) where T : ISerializable
    {
        if (WriteIsDefault(values))
        {
            return;
        }

        WriteValue(values!.Length);

        for (int i = 0; i < values.Length; i++)
        {
            WriteObject(values[i], includeType);
        }
    }

    public unsafe void WriteString(string? value)
    {
        WriteValues(value is null ? null : Encoding.GetBytes(value));
    }

    public void WriteStrings(string?[]? values)
    {
        if (WriteIsDefault(values))
        {
            return;
        }

        WriteValue(values!.Length);

        for (int i = 0; i < values.Length; i++)
        {
            WriteString(values[i]);
        }
    }

    public bool WriteIsDefault<T>(T? value)
    {
        if (Equals(value, default))
        {
            WriteValue(true);
            return true;
        }

        WriteValue(false);

        return false;
    }
}
