using System;
using System.IO;

namespace Vildmark.Serialization
{
	public interface IWriter
	{
		Stream BaseStream { get; }

		void WriteValue<T>(T value) where T : unmanaged;
		void WriteValues<T>(T[] values) where T : unmanaged;

		void WriteObject<T>(T value) where T : ISerializable, new();
		void WriteObjects<T>(T[] values) where T : ISerializable, new();

		void WriteString(string value);
		void WriteStrings(string[] values);
	}
}