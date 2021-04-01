using OpenTK.Mathematics;
using System;
using Vildmark.Maths;

namespace Vildmark.Graphics
{
    public class Transform
    {
        private Matrix4? matrix;

        private float scale;
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 origin;

        public Vector3 Position
        {
            get => position;
            set => SetValue(ref position, value);
        }

        public Vector3 Rotation
        {
            get => rotation;
            set => SetValue(ref rotation, value);
        }

        public Vector3 Origin
        {
            get => origin;
            set => SetValue(ref origin, value);
        }

        public float Scale
        {
            get => scale <= 0 ? (scale = 1) : scale;
            set => SetValue(ref scale, value);
        }

        public float X
        {
            get => position.X;
            set => SetValue(ref position.X, value);
        }

        public float Y
        {
            get => position.Y;
            set => SetValue(ref position.Y, value);
        }

        public float Z
        {
            get => position.Z;
            set => SetValue(ref position.Z, value);
        }

        public float RotationX
        {
            get => rotation.X;
            set => SetValue(ref rotation.X, value);
        }

        public float RotationY
        {
            get => rotation.Y;
            set => SetValue(ref rotation.Y, value);
        }

        public float RotationZ
        {
            get => rotation.Z;
            set => SetValue(ref rotation.Z, value);
        }

        public float OriginX
        {
            get => origin.X;
            set => SetValue(ref origin.X, value);
        }

        public float OriginY
        {
            get => origin.Y;
            set => SetValue(ref origin.Y, value);
        }

        public float OriginZ
        {
            get => origin.Z;
            set => SetValue(ref origin.Z, value);
        }

        public Vector3 ForwardVector => new(MathF.Sin(RotationY), 0, -MathF.Cos(RotationY));
        public Vector3 RightVector => new Vector3(MathF.Cos(RotationY), 0, MathF.Sin(RotationY));

        public Matrix4 Matrix => matrix ??= CreateMatrix();

        protected virtual Matrix4 CreateMatrix()
        {
            return MatrixHelper.CreateMatrix(Position, Rotation, Origin, Scale);
        }

        private void SetValue<T>(ref T field, T value)
        {
            field = value;
            matrix = null;
        }
    }
}
