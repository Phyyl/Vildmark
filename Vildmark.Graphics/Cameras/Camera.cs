using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Cameras
{
    public abstract class Camera : ICamera
    {
        private Matrix4? projectionMatrix;

        private int width;
        private int height;
        private float zNear;
        private float zFar;

        public Transform Transform { get; } = new CameraTransform();

        public int Width
        {
            get => width;
            set => SetValue(ref width, value);
        }

        public int Height
        {
            get => height;
            set => SetValue(ref height, value);
        }

        public float ZNear
        {
            get => zNear;
            set => SetValue(ref zNear, value);
        }

        public float ZFar
        {
            get => zFar;
            set => SetValue(ref zFar, value);
        }

        public RectangleF Viewport => new(Transform.X * Transform.Scale, Transform.Y * Transform.Scale, Width / Transform.Scale, Height / Transform.Scale);

        public Matrix4 ProjectionMatrix => projectionMatrix ??= CreateProjectionMatrix();

        public Matrix4 ViewMatrix => Transform.Matrix;

        public Matrix4 Matrix => ViewMatrix * ProjectionMatrix;

        public float AspectRatio => width / (float)height;

        protected Camera(int width, int height, float zNear, float zFar)
        {
            Width = width;
            Height = height;
            ZNear = zNear;
            ZFar = zFar;
        }

        protected abstract Matrix4 CreateProjectionMatrix();

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            projectionMatrix = null;
        }

        public virtual void Resize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public virtual void SetupShader(IShader shader)
        {
            if (shader is IProjectionMatrixShader projectionMatrixShader)
            {
                projectionMatrixShader.ProjectionMatrix.SetValue(ProjectionMatrix);
            }

            if (shader is IViewMatrixShader viewMatrixShader)
            {
                viewMatrixShader.ViewMatrix.SetValue(ViewMatrix);
            }
        }

        private class CameraTransform : Transform
        {
            protected override Matrix4 CreateMatrix()
            {
                return MatrixHelper.CreateMatrix(Origin, Rotation, Position, Scale);
            }
        }
    }
}
