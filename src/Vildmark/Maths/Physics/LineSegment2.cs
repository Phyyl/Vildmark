using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public struct LineSegment2(Vector2 start, Vector2 end)
{
    public Vector2 Start = start;
    public Vector2 End = end;

    public Vector2 Direction => Delta.Normalized();
    public Vector2 Delta => End - Start;
    public float Length => Delta.Length;

    public override string ToString()
    {
        return $"{Start} -> {End}";
    }
}
