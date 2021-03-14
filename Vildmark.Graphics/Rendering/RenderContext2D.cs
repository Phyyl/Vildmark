using Vildmark.Graphics.Cameras;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext2D : RenderContext
    {
        public OrthographicOffCenterCamera OrthographicCamera { get; }

        public override Camera Camera => OrthographicCamera;

        public RenderContext2D(int width = 1920, int height = 1080)
        {
            OrthographicCamera = new OrthographicOffCenterCamera(width, height);
        }
    }
}
