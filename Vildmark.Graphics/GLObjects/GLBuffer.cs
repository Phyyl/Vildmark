using OpenTK.Graphics.OpenGL;
using System;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.GLObjects
{
	public abstract class GLBuffer : GLObject
	{
		public BufferTarget BufferTarget { get; }

		public BufferUsageHint BufferUsageHint { get; }

		public int Count { get; protected set; }

		protected GLBuffer(BufferTarget bufferTarget, BufferUsageHint bufferUsageHint)
			: base(GL.GenBuffer())
		{
			BufferTarget = bufferTarget;
			BufferUsageHint = bufferUsageHint;
		}

		public IDisposable Bind()
		{
			return new BindContext<GLBuffer>(this);
		}

		public void Unbind()
		{
			GL.BindBuffer(BufferTarget, 0);
		}

		//public void Enable(int index, bool bind = true)
		//{
		//	if (index < 0)
		//	{
		//		return;
		//	}

		//	IDisposable bindContext = bind ? Bind() : default;

		//	GL.EnableVertexAttribArray(index);

		//	bindContext?.Dispose();
		//}

		//public void Disable(int index, bool bind = true)
		//{
		//	if (index < 0)
		//	{
		//		return;
		//	}

		//	IDisposable bindContext = bind ? Bind() : default;

		//	GL.DisableVertexAttribArray(index);

		//	bindContext?.Dispose();
		//}

		//public void VertexAttribPointer(int index, int size, VertexAttribPointerType vertexAttribPointerType = VertexAttribPointerType.Float, int stride = 0, int offset = 0, bool normalized = false, bool bind = true, bool enable = true, int divisor = 0)
		//{
		//	if (index < 0)
		//	{
		//		return;
		//	}

		//	IDisposable bindContext = bind ? Bind() : default;

		//	if (enable)
		//	{
		//		Enable(index, false);
		//	}

		//	if (vertexAttribPointerType >= VertexAttribPointerType.Byte && vertexAttribPointerType <= VertexAttribPointerType.UnsignedInt)
		//	{
		//		GL.VertexAttribIPointer(index, size, (VertexAttribIntegerType)vertexAttribPointerType, stride, (IntPtr)offset);
		//	}
		//	else
		//	{
		//		GL.VertexAttribPointer(index, size, vertexAttribPointerType, normalized, stride, offset);
		//	}

		//	if (divisor > 0)
		//	{
		//		GL.VertexAttribDivisor(index, divisor);
		//	}

		//	bindContext?.Dispose();
		//}

		protected override void DisposeOpenGL()
		{
			GL.DeleteBuffer(this);
		}

		protected class BindContext<T> : IDisposable where T : GLBuffer
		{
			public T Buffer { get; }

			public BindContext(T buffer, bool bind = true)
			{
				Buffer = buffer;

				if (bind)
				{
					GL.BindBuffer(buffer.BufferTarget, buffer);
				}
			}

			public virtual void Dispose()
			{
				Buffer.Unbind();
			}
		}
	}

	public class GLBuffer<T> : GLBuffer where T : unmanaged
	{
		public GLBuffer(int size = default, BufferTarget bufferTarget = BufferTarget.ArrayBuffer, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw)
			: this(new Span<T>(new T[size]), bufferTarget, bufferUsageHint)
		{
		}

		public GLBuffer(Span<T> data, BufferTarget bufferTarget = BufferTarget.ArrayBuffer, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw)
			: base(bufferTarget, bufferUsageHint)
		{
			SetData(data);
		}

		public unsafe void SetData(Span<T> data)
		{
			using (Bind())
			{
				if (data.IsEmpty)
				{
					GL.BufferData(BufferTarget, 0, IntPtr.Zero, BufferUsageHint);
				}
				else
				{
					GL.BufferData(BufferTarget, data.Length * sizeof(T), ref MemoryMarshal.GetReference(data), BufferUsageHint);
				}
			}

			Count = data.Length;
		}

		public IDisposable Map(out Span<T> data)
		{
			return Map(BufferAccess.ReadWrite, out data);
		}

		public IDisposable MapReadOnly(out Span<T> data)
		{
			return Map(BufferAccess.ReadOnly, out data);
		}

		public IDisposable MapWriteOnly(out Span<T> data)
		{
			return Map(BufferAccess.WriteOnly, out data);
		}

		public unsafe IDisposable Map(BufferAccess bufferAccess, out Span<T> data)
		{
			MapContext mapContext = new MapContext(this, bufferAccess);

			GL.GetBufferParameter(BufferTarget, BufferParameterName.BufferSize, out int size);

			data = new Span<T>(mapContext.Ptr.ToPointer(), size / sizeof(T));

			return mapContext;
		}

		public void Unmap()
		{
			GL.UnmapBuffer(BufferTarget);
		}

		protected class MapContext : BindContext<GLBuffer<T>>
		{
			public IntPtr Ptr { get; }

			public MapContext(GLBuffer<T> buffer, BufferAccess bufferAccess, bool map = true)
				: base(buffer, map)
			{
				if (map)
				{
					Ptr = GL.MapBuffer(buffer.BufferTarget, bufferAccess);
				}
			}

			public override void Dispose()
			{
				Buffer.Unmap();
			}
		}
	}
}
