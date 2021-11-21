using OpenTK.Graphics.OpenGL4;

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
}
