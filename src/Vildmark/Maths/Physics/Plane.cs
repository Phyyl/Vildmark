namespace Vildmark.Maths.Physics;

public struct Plane
{
    public Vector3 Normal;
    public float Distance;

    public Plane(Vector3 a, Vector3 b, Vector3 c)
    {
        Normal = Vector3.Cross(b - a, c - a).Normalized();
        Distance = Vector3.Dot(Normal, a);
    }
}
