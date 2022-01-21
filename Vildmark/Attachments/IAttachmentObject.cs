using System.Diagnostics.CodeAnalysis;

namespace Vildmark.Attachments
{
    public interface IAttachmentObject
	{
		object SetAttachment(string name, object value);
		T SetAttachment<T>(string name, T value) where T : notnull;
		T SetAttachment<T>(string name) where T : notnull, new();

		object? GetAttachment(string name);
		T? GetAttachment<T>(string name);
		bool TryGetAttachment(string name, [NotNullWhen(true)] out object? attachment);
		bool TryGetAttachment<T>(string name, [NotNullWhen(true)] out T? attachment);

		object? RemoveAttachment(string name);
	}
}
