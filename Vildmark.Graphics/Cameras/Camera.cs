using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;
using Vildmark.Maths;

namespace Vildmark.Graphics.Cameras
{
    public abstract class Camera
    {
        private Vector3 position;
        private Vector3 rotation;

        public ref Vector3 Position => ref position;
        public ref Vector3 Rotation => ref rotation;
        public float Scale { get; set; }

        public abstract Matrix4 ProjectionMatrix { get; }

        public Matrix4 ViewMatrix => MatrixHelper.CreateMatrix(-Position, -Rotation);

        public abstract void Resize(int width, int height);
    }
}
