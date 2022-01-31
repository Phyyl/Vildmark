namespace Vildmark.Serialization
{
    public interface ISerializable
    {
        void Serialize(IWriter writer);
    }

    public interface IDeserializable
    {
        void Deserialize(IReader reader);
    }
}
