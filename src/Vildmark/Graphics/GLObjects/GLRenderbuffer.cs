using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.GLObjects;

internal class GLRenderbuffer : GLObject
{
    public GLRenderbuffer(int width, int height, RenderbufferTarget renderbufferTarget = RenderbufferTarget.Renderbuffer, RenderbufferStorage renderbufferStorage = RenderbufferStorage.Rgba8)
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
        GL.RenderbufferStorage(RenderbufferTarget, RenderbufferStorage, width, height);
    }

    public static void Unbind(RenderbufferTarget renderbufferTarget)
    {
        GL.BindRenderbuffer(renderbufferTarget, 0);
    }
}
