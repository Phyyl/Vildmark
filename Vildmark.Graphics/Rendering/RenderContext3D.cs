using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext3D : RenderContext
    {
        public override ICamera Camera { get; }

        private RenderContext3D(ICamera camera)
        {
            Camera = camera;
        }

        public override void Begin(FrameBuffer frameBuffer = default, bool clear = true)
        {
            base.Begin(frameBuffer, clear);

            EnableDepthTest();
        }

        public static RenderContext3D CreateOrthographic(int width = 1920, int height = 1080, float zNear = 0.01f, float zFar = 1000) => new(new OrthographicCamera(width, height, zNear, zFar));
        public static RenderContext3D CreatePerspective(float fovY = MathHelper.PiOver3, int width = 1920, int height = 1080, float zNear = 0.01f, float zFar = 1000) => new(new PerspectiveCamera(fovY, width, height, zNear, zFar));
    }
}
