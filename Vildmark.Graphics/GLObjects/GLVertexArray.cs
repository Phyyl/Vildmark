using OpenToolkit.Graphics.OpenGL;
using System;

namespace Vildmark.Graphics.GLObjects
{
	public class GLVertexArray : GLObject
	{
		public GLVertexArray()
			: base(GL.GenVertexArray())
		{

		}

		public IDisposable Bind()
		{
			return new BindContext(this);
		}

		public static void Unbind()
		{
			GL.BindVertexArray(0);
		}

		protected override void DisposeOpenGL()
		{
			GL.DeleteVertexArray(this);
		}

		private class BindContext : IDisposable
		{
			public GLVertexArray VertexArray { get; }

			public BindContext(GLVertexArray vertexArray, bool bind = true)
			{
				VertexArray = vertexArray;

				if (bind)
				{
					GL.BindVertexArray(VertexArray);
				}
			}

			public virtual void Dispose()
			{
				Unbind();
			}
		}
	}
}
