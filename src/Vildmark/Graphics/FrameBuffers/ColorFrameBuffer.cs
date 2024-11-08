using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.FrameBuffers;

public class ColorFrameBuffer(int width, int height) : FrameBuffer(width, height, FramebufferAttachment.ColorAttachment0, Texture2DParameters.Texture2D)
{
    internal GLRenderbuffer? GLRenderbuffer { get; private set; }

    protected override void InitializeDrawBuffer(int width, int height)
    {
        GLRenderbuffer = new GLRenderbuffer(width, height);

        GLFramebuffer.SetRenderbuffer(GLRenderbuffer, FramebufferAttachment.ColorAttachment0);
    }

    protected override void InitializeReadBuffer(int width, int height)
    {
    }

    public virtual void Render(Renderer renderContext, IShader? shader = default)
    {
        renderContext.RenderRectangle(new Box2(0, 0, renderContext.Width, renderContext.Height), new Texture2D(GLTexture), new Transform() { OriginY = renderContext.Height / 2f, RotationX = MathF.PI });
    }
}
