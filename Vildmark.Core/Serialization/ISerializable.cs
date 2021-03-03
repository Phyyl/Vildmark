namespace Vildmark.Serialization
{
	public interface ISerializable
	{
		void Serialize(IWriter writer);
		void Deserialize(IReader reader);
	}
}