using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.FrameBuffers;

public abstract class FrameBuffer
{
    protected FrameBuffer(
        int width,
        int height,
        FramebufferAttachment framebufferAttachment,
        Texture2DParameters parameters)
    {
        GLFramebuffer = new GLFramebuffer();
        GLTexture = new GLTexture2D(width, height, default, parameters);

        GLFramebuffer.Bind();
        GLFramebuffer.SetTexture(GLTexture, framebufferAttachment);
        InitializeReadBuffer(width, height);
        InitializeDrawBuffer(width, height);
        GLFramebuffer.Unbind();
    }

    internal GLFramebuffer GLFramebuffer { get; }
    internal GLTexture2D GLTexture { get; }

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
