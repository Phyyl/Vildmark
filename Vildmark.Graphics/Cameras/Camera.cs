using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
    public abstract class Camera
    {
        private Vector3 translate;
        private Vector3 rotation;

        public ref Vector3 Translation => ref translate;
        public ref Vector3 Rotation => ref rotation;

        public abstract Matrix4 ProjectionMatrix { get; }

        public Matrix4 ViewMatrix
        {
            get
            {
                Matrix4 matrix = translate.LengthSquared > 0 ? Matrix4.CreateTranslation(translate) : Matrix4.Identity;

                if (rotation.Y > 0)
                {
                    matrix *= Matrix4.CreateRotationY(Rotation.Y);
                }

                if (rotation.X > 0)
                {
                    matrix *= Matrix4.CreateRotationY(Rotation.X);
                }

                if (rotation.Z > 0)
                {
                    matrix *= Matrix4.CreateRotationY(Rotation.Z);
                }

                return matrix;
            }
        }

        public virtual void Resize(int width, int height)
        {
        }
    }
}
