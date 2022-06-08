using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public struct LineSegment2
{
    public Vector2 Start;
    public Vector2 End;

    public Vector2 Direction => Delta.Normalized();
    public Vector2 Delta => End - Start;
    public float Length => Delta.Length;

    public LineSegment2(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }

    public override string ToString()
    {
        return $"{Start} -> {End}";
    }
}
