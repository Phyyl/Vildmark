using OpenTK.Graphics.OpenGL;

namespace Vildmark.Graphics.GLObjects;

internal class GLFramebuffer(FramebufferTarget framebufferTarget = FramebufferTarget.Framebuffer) : GLObject(GL.GenFramebuffer())
{
    public FramebufferTarget FramebufferTarget { get; } = framebufferTarget;

    public bool Complete => GL.CheckFramebufferStatus(FramebufferTarget) == FramebufferStatus.FramebufferComplete;

    protected override void DisposeOpenGL(ref int id)
    {
        GL.DeleteFramebuffer(ref id);
    }

    public void Bind()
    {
        GL.BindFramebuffer(FramebufferTarget, this);
    }

    public void Unbind()
    {
        Unbind(FramebufferTarget);
    }

    public void SetRenderbuffer(GLRenderbuffer renderbuffer, FramebufferAttachment framebufferAttachment)
    {
        GL.FramebufferRenderbuffer(FramebufferTarget, framebufferAttachment, renderbuffer.RenderbufferTarget, renderbuffer);
    }

    public void SetTexture(GLTexture2D texture, FramebufferAttachment framebufferAttachment)
    {
        GL.FramebufferTexture(FramebufferTarget, framebufferAttachment, texture, 0);
    }

    public void DrawBuffer(DrawBufferMode drawBufferMode)
    {
        GL.DrawBuffer(drawBufferMode);
    }

    public void ReadBuffer(ReadBufferMode readBufferMode)
    {
        GL.ReadBuffer(readBufferMode);
    }

    public static void Unbind(FramebufferTarget target)
    {
        GL.BindFramebuffer(target, 0);
    }
}
