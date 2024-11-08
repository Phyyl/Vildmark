using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public struct Ray(Vector3 start, Vector3 direction)
{
    public Vector3 Start = start;
    public Vector3 Direction = direction.Normalized();

    public Ray Offset(Vector3 offset)
    {
        return new Ray(Start + offset, Direction);
    }

    public override string ToString()
    {
        return $"{Start}, {Direction}";
    }
}
