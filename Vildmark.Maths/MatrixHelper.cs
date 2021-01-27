using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Maths
{
    public static class MatrixHelper
    {
        public static Matrix4 CreateMatrix(Vector3 position, Vector3 rotation, Vector3 origin = default)
        {
            Matrix4 result = Matrix4.Identity;

            if (origin != default)
            {
                result *= Matrix4.CreateTranslation(-origin);
            }

            if (rotation.Y != 0)
            {
                result *= Matrix4.CreateRotationY(rotation.Y);
            }

            if (rotation.X != 0)
            {
                result *= Matrix4.CreateRotationX(rotation.X);
            }

            if (rotation.Z != 0)
            {
                result *= Matrix4.CreateRotationZ(rotation.Z);
            }

            if (position != default)
            {
                result *= Matrix4.CreateTranslation(position);
            }

            return result;
        }
    }
}
