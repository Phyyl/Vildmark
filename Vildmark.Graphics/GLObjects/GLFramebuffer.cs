using OpenToolkit.Graphics.OpenGL;
using System;

namespace Vildmark.Graphics.GLObjects
{
	public class GLFramebuffer : GLObject
	{
		public GLFramebuffer(FramebufferTarget framebufferTarget = FramebufferTarget.Framebuffer)
			: base(GL.GenFramebuffer())
		{
			FramebufferTarget = framebufferTarget;
		}

		public FramebufferTarget FramebufferTarget { get; }

		public bool Complete => GL.CheckFramebufferStatus(FramebufferTarget) == FramebufferErrorCode.FramebufferComplete;

		protected override void DisposeOpenGL()
		{
			GL.DeleteFramebuffer(this);
		}

		public IDisposable Bind()
		{
			return new BindContext(this);
		}

		public void Unbind()
		{
			GL.BindFramebuffer(FramebufferTarget, 0);
		}

		public void SetRenderbuffer(GLRenderbuffer renderbuffer, FramebufferAttachment framebufferAttachment)
		{
			using (Bind())
			{
				GL.FramebufferRenderbuffer(FramebufferTarget, framebufferAttachment, renderbuffer.RenderbufferTarget, renderbuffer);
			}
		}

		public void SetTexture(GLTexture2D texture, FramebufferAttachment framebufferAttachment)
		{
			using (Bind())
			{
				GL.FramebufferTexture(FramebufferTarget, framebufferAttachment, texture, 0);
			}
		}

		public void DrawBuffer(GLRenderbuffer renderbuffer, DrawBufferMode drawBufferMode)
		{
			using (Bind())
			{
				using (renderbuffer.Bind())
				{
					GL.DrawBuffer(drawBufferMode);
				}
			}
		}

		private class BindContext : IDisposable
		{
			public GLFramebuffer Framebuffer { get; }

			public BindContext(GLFramebuffer framebuffer, bool bind = true)
			{
				Framebuffer = framebuffer;

				if (bind)
				{
					GL.BindFramebuffer(Framebuffer.FramebufferTarget, Framebuffer);
				}
			}

			public void Dispose()
			{
				Framebuffer.Unbind();
			}
		}
	}
}
