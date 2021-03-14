using OpenTK.Graphics.OpenGL;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class FrameBuffer
	{
		public FrameBuffer(int width, int height)
		{
			GLFramebuffer = new GLFramebuffer();
			GLTexture = new GLTexture2D(width, height, options: TextureLoadOptions.Nearest);
			GLRenderbuffer = new GLRenderbuffer(width, height);

			GLFramebuffer.SetRenderbuffer(GLRenderbuffer, FramebufferAttachment.DepthAttachment);
			GLFramebuffer.SetTexture(GLTexture, FramebufferAttachment.ColorAttachment0);
			GLFramebuffer.DrawBuffer(GLRenderbuffer, DrawBufferMode.ColorAttachment0);
		}

		public GLFramebuffer GLFramebuffer { get; }

		public GLRenderbuffer GLRenderbuffer { get; }

		public GLTexture2D GLTexture { get; }

		public void Resize(int width, int height)
		{
			GLTexture.Resize(width, height);
			GLRenderbuffer.Resize(width, height);
		}

		public void Bind()
		{
            GLFramebuffer.Bind();

			GL.Viewport(0, 0, GLTexture.Width, GLTexture.Height);
		}

		public void Unbind()
		{
			GLFramebuffer.Unbind();
		}
	}
}
