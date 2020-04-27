using OpenToolkit.Graphics.OpenGL;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
	public class Attrib<T> : ShaderVariable where T : unmanaged
	{
		public Attrib(Shader shader, string name, int size, VertexAttribPointerType vertexAttribPointerType, int stride = 0, int offset = 0, bool normalized = false)
			: base(shader, name)
		{
			Size = size;
			VertexAttribPointerType = vertexAttribPointerType;
			Stride = stride;
			Offset = offset;
			Normalized = normalized;
		}

		public int Size { get; }

		public VertexAttribPointerType VertexAttribPointerType { get; }

		public int Stride { get; }

		public int Offset { get; }

		public bool Normalized { get; }

		public void Enable(GLBuffer<T> buffer, bool bind = true)
		{
			if (!Defined)
			{
				return;
			}

			buffer.Enable(Location, bind);
		}

		public void Disable(GLBuffer<T> buffer, bool bind = true)
		{
			if (!Defined)
			{
				return;
			}

			buffer.Disable(Location, bind);
		}

		public void VertexAttribPointer(GLBuffer<T> buffer, bool enable = true, bool bind = true, int divisor = 0)
		{
			if (!Defined)
			{
				return;
			}

			buffer.VertexAttribPointer(Location, Size, VertexAttribPointerType, Stride, Offset, Normalized, bind, enable, divisor);
		}

		protected override int GetLocation() => Shader.GetAttribLocation(Name);
	}
}
