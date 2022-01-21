using OpenTK.Mathematics;

namespace Vildmark.Graphics.Cameras
{
    public class OrthographicCamera : Camera
    {
        public OrthographicCamera(int width, int height, float zNear = 1, float zFar = -1)
            : base(width, height, zNear, zFar)
        {
        }

        protected override Matrix4 CreateProjectionMatrix()
        {
            return Matrix4.CreateOrthographic(Width, Height, ZNear, ZFar);
        }
    }
}
