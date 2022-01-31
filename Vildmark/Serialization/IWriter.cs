namespace Vildmark.Serialization
{
    public interface IWriter
    {
        Stream BaseStream { get; }

        bool WriteIsDefault<T>(T? value);

        void WriteValue<T>(T value) where T : unmanaged;
        void WriteValues<T>(T[]? values) where T : unmanaged;
        void WriteValues<T>(T[,]? values) where T : unmanaged;
        void WriteValues<T>(T[,,]? values) where T : unmanaged;
        public void WriteValues<T>(Span<T> span) where T : unmanaged;

        void WriteObject<T>(T? value, bool includeType = false) where T : ISerializable;
        void WriteObjects<T>(T?[]? values, bool includeType = false) where T : ISerializable;

        void WriteString(string? value);
        void WriteStrings(string?[]? values);
    }
}
