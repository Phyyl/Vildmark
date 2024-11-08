using OpenTK.Graphics.OpenGL;

namespace Vildmark.Graphics.GLObjects;

internal class GLRenderbuffer : GLObject
{
    public GLRenderbuffer(int width, int height, RenderbufferTarget renderbufferTarget = RenderbufferTarget.Renderbuffer, InternalFormat internalFormat = InternalFormat.Rgba8)
        : base(GL.GenRenderbuffer())
    {
        RenderbufferTarget = renderbufferTarget;
        InternalFormat = internalFormat;

        Resize(width, height);
    }

    public RenderbufferTarget RenderbufferTarget { get; }

    public InternalFormat InternalFormat { get; }

    protected override void DisposeOpenGL(ref int id)
    {
        GL.DeleteRenderbuffer(ref id);
    }

    public void Bind()
    {
        GL.BindRenderbuffer(RenderbufferTarget, this);
    }

    public void Unbind()
    {
        Unbind(RenderbufferTarget);
    }

    public void Resize(int width, int height)
    {
        GL.RenderbufferStorage(RenderbufferTarget, InternalFormat, width, height);
    }

    public static void Unbind(RenderbufferTarget renderbufferTarget)
    {
        GL.BindRenderbuffer(renderbufferTarget, 0);
    }
}
