using Vildmark.Graphics.Cameras;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext2D : RenderContext
    {
        private readonly ICamera camera;

        public override ICamera Camera => camera;

        private RenderContext2D(ICamera camera)
        {
            this.camera = camera;
        }

        public static RenderContext2D Create(int width = 1920, int height = 1080, float zNear = 1, float zFar = -1)
        {
            return new RenderContext2D(new OrthographicOffCenterCamera(width, height, zNear, zFar));
        }
    }
}
