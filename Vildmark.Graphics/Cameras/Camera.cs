using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
    public abstract class Camera
    {
        private Vector3 translation;
        private Vector3 rotation;
        private float scale;

        public ref Vector3 Translation => ref translation;
        public ref Vector3 Rotation => ref rotation;

        public ref float Scale => ref scale;

        public abstract Matrix4 ProjectionMatrix { get; }

        public Matrix4 ViewMatrix
        {
            get
            {
                Matrix4 matrix = translation.LengthSquared > 0 ? Matrix4.CreateTranslation(translation) : Matrix4.Identity;

                if (scale != 1)
                {
                    matrix *= Matrix4.CreateScale(scale);
                }

                if (rotation.Y > 0)
                {
                    matrix *= Matrix4.CreateRotationY(rotation.Y);
                }

                if (rotation.X > 0)
                {
                    matrix *= Matrix4.CreateRotationY(rotation.X);
                }

                if (rotation.Z > 0)
                {
                    matrix *= Matrix4.CreateRotationY(rotation.Z);
                }

                return matrix;
            }
        }

        public virtual void Resize(int width, int height)
        {
        }
    }
}
