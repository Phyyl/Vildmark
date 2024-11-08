using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public struct LineSegment3(Vector3 start, Vector3 end)
{
    public Vector3 Start = start;
    public Vector3 End = end;

    public Vector3 Direction => Delta.Normalized();
    public Vector3 Delta => End - Start;
    public float Length => Delta.Length;

    public override string ToString()
    {
        return $"{Start}, {End}";
    }
}
