using OpenTK.Graphics.OpenGL;
using System;

namespace Vildmark.Graphics.GLObjects
{
	public class GLVertexArray : GLObject
	{
		public GLVertexArray()
			: base(GL.GenVertexArray())
		{

		}

		public void Bind()
        {
            GL.BindVertexArray(this);
        }

		public static void Unbind()
		{
			GL.BindVertexArray(0);
		}

		protected override void DisposeOpenGL()
		{
			GL.DeleteVertexArray(this);
		}
	}
}
