using OpenTK.Mathematics;

namespace Vildmark.Maths;

public static class MatrixHelper
{
    //TODO: Check if rotation is scaled
    public static Matrix4 CreateMatrix(Vector3 position, Vector3 rotation, Vector3 origin, Vector3 scale)
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

        if (scale != Vector3.One)
        {
            result *= Matrix4.CreateScale(scale);
        }

        return result;
    }
}
