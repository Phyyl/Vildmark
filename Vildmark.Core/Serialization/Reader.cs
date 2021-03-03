using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vildmark.Serialization
{
	public class Reader : IReader
	{
		public Stream BaseStream { get; }

		public Reader(Stream stream)
		{
			BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
		}

		public T ReadObject<T>() where T : ISerializable, new()
		{
			T result = new T();

			result.Deserialize(this);

			return result;
		}

		public T[] ReadObjects<T>() where T : ISerializable, new()
		{
			if (ReadIsDefault())
			{
				return default;
			}

			T[] result = new T[ReadValue<int>()];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = ReadObject<T>();
			}

			return result;
		}

		public string ReadString()
		{
			if (ReadIsDefault())
			{
				return default;
			}

			return new string(ReadValues<char>());
		}

		public string[] ReadStrings()
		{
			if (ReadIsDefault())
			{
				return default;
			}

			string[] result = new string[ReadValue<int>()];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = ReadString();
			}

			return result;
		}

		public unsafe T ReadValue<T>() where T : unmanaged
		{
			T result = new T();

			ReadRaw(new Span<T>(&result, 1));

			return result;
		}

		public T[] ReadValues<T>() where T : unmanaged
		{
			if (ReadIsDefault())
			{
				return default;
			}

			T[] result = new T[ReadValue<int>()];

			ReadRaw(result.AsSpan());

			return result;
		}

		private bool ReadIsDefault()
		{
			return ReadValue<bool>();
		}

		private unsafe void ReadRaw<T>(Span<T> span) where T : unmanaged
		{
			Span<byte> buffer = MemoryMarshal.Cast<T, byte>(span);

			BaseStream.Read(buffer);
		}
	}
}