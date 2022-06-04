namespace Vildmark.Serialization;

public interface IReader
{
    Stream BaseStream { get; }

    bool ReadIsDefault();

    T ReadValue<T>() where T : unmanaged;
    T[]? ReadValues<T>() where T : unmanaged;
    T[,]? Read2DValues<T>() where T : unmanaged;
    T[,,]? Read3DValues<T>() where T : unmanaged;

    T? ReadObject<T>(bool includeType = false);
    T?[]? ReadObjects<T>(bool includeType = false);

    string? ReadString();
    string?[]? ReadStrings();
}
