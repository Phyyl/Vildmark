using Vildmark.Graphics.Cameras;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext3D : RenderContext
    {
        public PerspectiveCamera PerspectiveCamera { get; }

        public override Camera Camera => PerspectiveCamera;

        public RenderContext3D(int width, int height, float fovY)
        {
            PerspectiveCamera = new PerspectiveCamera(fovY, width, height);
        }

        public override void Begin()
        {
            base.Begin();

            EnableDepthTest();
        }
    }
}
