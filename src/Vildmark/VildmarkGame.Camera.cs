using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;

namespace Vildmark;

public partial class VildmarkGame
{
    protected static OrthographicOffCenterCamera CreateAutoOrthographicOffCenterCamera(float zNear = 1, float zFar = -1)
    {
        OrthographicOffCenterCamera camera = new(0, Width, Height, 0, zNear, zFar);

        Window.Resize += e =>
        {
            camera.Right = e.Width;
            camera.Bottom = e.Height;
        };

        return camera;
    }

    protected static OrthographicCamera CreateAutoOrthographicCamera(float zNear = 1, float zFar = -1)
    {
        OrthographicCamera camera = new(Width, Height, zNear, zFar);

        Window.Resize += e =>
        {
            camera.Width = e.Width;
            camera.Height = e.Height;
        };

        return camera;
    }

    protected static PerspectiveCamera CreateAutoPerspectiveCamera(float fovY = MathF.PI / 3f, float zNear = 0.01f, float zFar = 1000)
    {
        PerspectiveCamera camera = new(AspectRatio, fovY, zNear, zFar);

        Window.Resize += e =>
        {
            camera.AspectRatio = AspectRatio;
        };

        return camera;
    }
}
