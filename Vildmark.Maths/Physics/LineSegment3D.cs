using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics
{
    public struct LineSegment3D
    {
        public Vector3 Start;
        public Vector3 End;

        public Vector3 Direction => Movement.Normalized();
        public Vector3 Movement => End - Start;
        public float Length => Movement.Length;

        public LineSegment3D(Vector3 start, Vector3 end)
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
