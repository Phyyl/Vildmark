using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
    public class OrthographicOffCenterCamera : Camera
    {
        public OrthographicOffCenterCamera(int width, int height, float zNear = 1, float zFar = -1)
            : base(width, height, zNear, zFar)
        {
        }

        protected override Matrix4 CreateProjectionMatrix()
        {
            return Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, ZNear, ZFar);
        }
    }
}
