using OpenTK.Graphics.OpenGL;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public abstract class FrameBuffer
    {
        protected FrameBuffer(int width, int height, FramebufferAttachment framebufferAttachment, TextureOptions options)
        {
            GLFramebuffer = new GLFramebuffer();
            GLTexture = new GLTexture2D(width, height, options: options);

            GLFramebuffer.Bind();
            GLFramebuffer.SetTexture(GLTexture, framebufferAttachment);
            InitializeReadBuffer(width, height);
            InitializeDrawBuffer(width, height);
            GLFramebuffer.Unbind();
        }

        public GLFramebuffer GLFramebuffer { get; }

        public GLTexture2D GLTexture { get; }

        protected abstract void InitializeReadBuffer(int width, int height);
        protected abstract void InitializeDrawBuffer(int width, int height);

        public virtual void Resize(int width, int height)
        {
            GLTexture.Resize(width, height);
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
