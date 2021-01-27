using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Maths;

namespace Vildmark.Graphics
{
    public class Transform
    {
        private Matrix4? matrix;

        private Vector3 position;
        private Vector3 rotation;

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

        public Matrix4 Matrix => matrix ??= CreateMatrix();

        protected virtual Matrix4 CreateMatrix()
        {
            return MatrixHelper.CreateMatrix(Position, Rotation);
        }

        private void SetValue<T>(ref T field, T value)
        {
            field = value;
            matrix = null;
        }
    }
}
