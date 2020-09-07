using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Shaders
{
	public class Attrib<T> : ShaderVariable where T : unmanaged
	{
		public const int PositionIndex = 0;
		public const int ColorIndex = 1;
		public const int TexCoordIndex = 2;
		public const int NormalIndex = 3;

		public Attrib(IShader shader, string name, int size = 0, VertexAttribPointerType vertexAttribPointerType = VertexAttribPointerType.Float)
			: base(shader, name)
		{
			Size = size > 0 ? size : Marshal.SizeOf<T>() / 4;
			VertexAttribPointerType = vertexAttribPointerType;
		}

		public int Size { get; }

		public VertexAttribPointerType VertexAttribPointerType { get; }

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

		public void VertexAttribPointer(GLBuffer buffer, int stride = 0, int offset = 0, bool normalized = false, bool enable = true, bool bind = true, int divisor = 0)
		{
			if (!Defined)
			{
				return;
			}

			buffer.VertexAttribPointer(Location, Size, VertexAttribPointerType, stride, offset, normalized, bind, enable, divisor);
		}

		protected override int GetLocation() => Shader.GetAttribLocation(Name);

		public override string ToString()
		{
			return $"{base.ToString()}, Size: {Size}, VertexAttribPointerType: {VertexAttribPointerType}";
		}
	}
}
