using System.IO;

namespace Vildmark.Serialization
{
    public interface IReader
    {
        Stream BaseStream { get; }

        bool ReadIsDefault();

        T ReadValue<T>() where T : unmanaged;
        T[]? ReadValues<T>() where T : unmanaged;

        T? ReadObject<T>() where T : ISerializable, new();
        T?[]? ReadObjects<T>() where T : ISerializable, new();

        string? ReadString();
        string?[]? ReadStrings();
    }
}
