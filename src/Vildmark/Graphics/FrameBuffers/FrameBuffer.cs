using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.FrameBuffers;

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
        GLTexture.Configure(TextureMagFilter.Nearest, TextureMinFilter.Nearest, TextureWrapMode.ClampToBorder, TextureWrapMode.ClampToBorder);

        GLFramebuffer.Bind();
        GLFramebuffer.SetTexture(GLTexture, framebufferAttachment);
        InitializeReadBuffer(width, height);
        InitializeDrawBuffer(width, height);
        GLFramebuffer.Unbind();
    }

    internal GLFramebuffer GLFramebuffer { get; }
    internal GLTexture2D GLTexture { get; }

    public Texture2D Texture => new(GLTexture);

    public int Width => GLTexture.Width;
    public int Height => GLTexture.Height;
    public Vector2 Size => GLTexture.Size;
    public Box2 Bounds => new(0, 0, Width, Height);

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
