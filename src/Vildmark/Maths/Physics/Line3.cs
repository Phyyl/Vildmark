using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public struct LineSegment3
{
    public Vector3 Start;
    public Vector3 End;

    public Vector3 Direction => Delta.Normalized();
    public Vector3 Delta => End - Start;
    public float Length => Delta.Length;

    public LineSegment3(Vector3 start, Vector3 end)
    {
        Start = start;
        End = end;
    }

    public override string ToString()
    {
        return $"{Start}, {End}";
    }
}
