namespace Vildmark.Graphics.Cameras;

public class AutomaticOrthographicOffCenterCamera : OrthographicOffCenterCamera
{
    public AutomaticOrthographicOffCenterCamera(float zNear = 1, float zFar = -1)
        : base(0, VildmarkGame.Width, VildmarkGame.Height, 0, zNear, zFar)
    {
        VildmarkGame.Window.Resize += e =>
        {
            Right = e.Width;
            Bottom = e.Height;
        };
    }
}
