using System.Diagnostics.CodeAnalysis;

namespace Vildmark.Attachments
{
    public class AttachmentObject : IAttachmentObject
	{
		private readonly Dictionary<string, object> attachments = new();

		public object? GetAttachment(string name)
		{
			return attachments.GetValueOrDefault(name);
		}

		public T? GetAttachment<T>(string name)
		{
			return GetAttachment(name) is T t ? t : default;
		}

		public object? RemoveAttachment(string name)
		{
			if (attachments.TryGetValue(name, out object? value))
			{
				attachments.Remove(name);
				return value;
			}

			return default;
		}

		public object SetAttachment(string name, object value)
		{
			return attachments[name] = value;
		}

		public T SetAttachment<T>(string name, T value)
            where T : notnull
		{
			SetAttachment(name, (object)value);

			return value;
		}

		public T SetAttachment<T>(string name) where T : notnull, new()
        {
			return SetAttachment(name, new T());
		}

		public bool TryGetAttachment(string name, [NotNullWhen(true)] out object? attachment)
		{
			return attachments.TryGetValue(name, out attachment);
		}

		public bool TryGetAttachment<T>(string name, [NotNullWhen(true)] out T? attachment)
		{
			if (TryGetAttachment(name, out object? obj) && obj is T t)
			{
				attachment = t;
				return true;
			}

			attachment = default;
			return false;
		}
	}
}
