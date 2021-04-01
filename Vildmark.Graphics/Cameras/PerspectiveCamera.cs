using OpenTK.Mathematics;

namespace Vildmark.Graphics.Cameras
{
    public class PerspectiveCamera : Camera
    {
        private float fovY;

        public float FovY
        {
            get => fovY;
            set => SetValue(ref fovY, value);
        }

        public PerspectiveCamera(float fovY, int width, int height, float zNear = 0.01f, float zFar = 1000)
            : base(width, height, zNear, zFar)
        {
            FovY = fovY;
        }

        protected override Matrix4 CreateProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(FovY, AspectRatio, ZNear, ZFar);
        }
    }
}
