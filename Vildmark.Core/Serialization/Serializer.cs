using System.IO;

namespace Vildmark.Serialization
{
    public class Serializer
    {
        protected virtual IReader CreateReader(Stream stream) => new Reader(stream);
        protected virtual IWriter CreateWriter(Stream stream) => new Writer(stream);

        public virtual byte[] Serialize<T>(T value) where T : ISerializable, new()
        {
            using MemoryStream ms = new();
            IWriter writer = CreateWriter(ms);

            writer.WriteObject(value);

            return ms.ToArray();
        }

        public virtual T Deserialize<T>(byte[] data) where T : ISerializable, new()
        {
            using MemoryStream ms = new(data);
            IReader reader = CreateReader(ms);

            return reader.ReadObject<T>();
        }
    }
}