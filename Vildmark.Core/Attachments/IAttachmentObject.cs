namespace Vildmark.Attachments
{
    public interface IAttachmentObject
	{
		object SetAttachment(string name, object value);
		T SetAttachment<T>(string name, T value);
		T SetAttachment<T>(string name) where T : new();

		object GetAttachment(string name);
		T GetAttachment<T>(string name);
		bool TryGetAttachment(string name, out object attachment);
		bool TryGetAttachment<T>(string name, out T attachment);

		object RemoveAttachment(string name);
	}
}
