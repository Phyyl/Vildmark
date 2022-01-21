using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public abstract class FrameBuffer
    {
        protected FrameBuffer(
            int width,
            int height,
            FramebufferAttachment framebufferAttachment,
            Texture2DFormat format)
        {
            GLFramebuffer = new GLFramebuffer();
            GLTexture = new GLTexture2D(width, height, default, format.PixelFormat, format.PixelInternalFormat, format.PixelType);

            GLFramebuffer.Bind();
            GLFramebuffer.SetTexture(GLTexture, framebufferAttachment);
            InitializeReadBuffer(width, height);
            InitializeDrawBuffer(width, height);
            GLFramebuffer.Unbind();
        }

        public GLFramebuffer GLFramebuffer { get; }

        public GLTexture2D GLTexture { get; }

        public int Width => GLTexture.Width;
        public int Height => GLTexture.Height;
        public Vector2 Size => GLTexture.Size;

        protected abstract void InitializeReadBuffer(int width, int height);
        protected abstract void InitializeDrawBuffer(int width, int height);

        public virtual void Resize(int width, int height)
        {
            GLTexture.Resize(width, height);
        }

        public void Bind()
        {
            GLFramebuffer.Bind();
        }

        public void Unbind()
        {
            GLFramebuffer.Unbind();
        }
    }
}
