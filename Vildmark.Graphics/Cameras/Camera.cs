using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;
using Vildmark.Maths;

namespace Vildmark.Graphics.Cameras
{
    public abstract class Camera
    {
        private Vector3 position;
        private Vector3 rotation;
        private float scale = 1;

        public ref Vector3 Position => ref position;
        public ref Vector3 Rotation => ref rotation;

        public ref float Scale => ref scale;

        public abstract Matrix4 ProjectionMatrix { get; }

        public Matrix4 ViewMatrix => MatrixHelper.CreateViewMatrix(position, rotation, new Vector3(scale));

        public virtual void Resize(int width, int height)
        {
        }
    }
}
