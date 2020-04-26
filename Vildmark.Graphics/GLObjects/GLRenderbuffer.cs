using OpenToolkit.Graphics.OpenGL;
using System;

namespace Vildmark.Graphics.GLObjects
{
	public class GLRenderbuffer : GLObject
	{
		public GLRenderbuffer(int width, int height, RenderbufferTarget renderbufferTarget = RenderbufferTarget.Renderbuffer, RenderbufferStorage renderbufferStorage = RenderbufferStorage.DepthComponent)
			: base(GL.GenRenderbuffer())
		{
			RenderbufferTarget = renderbufferTarget;
			RenderbufferStorage = renderbufferStorage;

			Resize(width, height);
		}

		public RenderbufferTarget RenderbufferTarget { get; }

		public RenderbufferStorage RenderbufferStorage { get; }

		protected override void DisposeOpenGL()
		{
			GL.DeleteRenderbuffer(this);
		}

		public IDisposable Bind()
		{
			return new BindContext(this);
		}

		public void Unbind()
		{
			GL.BindRenderbuffer(RenderbufferTarget, this);
		}

		public void Resize(int width, int height)
		{
			using (Bind())
			{
				GL.RenderbufferStorage(RenderbufferTarget, RenderbufferStorage, width, height);
			}
		}

		private class BindContext : IDisposable
		{
			public GLRenderbuffer Renderbuffer { get; }

			public BindContext(GLRenderbuffer renderbuffer, bool bind = true)
			{
				Renderbuffer = renderbuffer;

				if (bind)
				{
					GL.BindRenderbuffer(Renderbuffer.RenderbufferTarget, Renderbuffer);
				}
			}

			public void Dispose()
			{
				Renderbuffer.Unbind();
			}
		}
	}
}
