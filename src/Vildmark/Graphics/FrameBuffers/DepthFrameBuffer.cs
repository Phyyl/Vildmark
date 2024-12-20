using OpenTK.Graphics.OpenGL;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.FrameBuffers;

public class DepthFrameBuffer(int width, int height) : FrameBuffer(width, height, FramebufferAttachment.DepthAttachment, Texture2DParameters.Depth)
{
    protected override void InitializeDrawBuffer(int width, int height)
    {
        GLFramebuffer.DrawBuffer(DrawBufferMode.None);
    }

    protected override void InitializeReadBuffer(int width, int height)
    {
        GLFramebuffer.ReadBuffer(ReadBufferMode.None);
    }
}
