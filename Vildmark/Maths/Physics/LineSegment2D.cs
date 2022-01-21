using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics
{
    public struct LineSegment2D
    {
        public Vector2 Start;
        public Vector2 End;

        public Vector2 Direction => Movement.Normalized();
        public Vector2 Movement => End - Start;
        public float Length => Movement.Length;

        public LineSegment2D(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return $"{Start}, {End}";
        }
    }
}
