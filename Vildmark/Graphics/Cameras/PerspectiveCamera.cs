using OpenTK.Mathematics;
using Vildmark.Maths;

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

        public PerspectiveCamera(int width, int height, float fovY = MathF.PI / 3f, float zNear = 0.01f, float zFar = 1000)
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
