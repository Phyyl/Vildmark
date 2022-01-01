using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext3D : RenderContext
    {
        public RenderContext3D(Camera camera)
            : base(camera)
        {
        }

        public override void Begin(FrameBuffer? frameBuffer = default, bool clear = true)
        {
            base.Begin(frameBuffer, clear);

            EnableDepthTest();
            EnableBlending();
        }
    }
}
