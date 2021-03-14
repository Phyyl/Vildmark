using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics
{
    public class AABBIntersectionResult
    {
        public Vector3 Position { get; }
        public Vector3 Movement { get; }
        public Vector3 Normal { get; }
        public AABBFace Face { get; }
        public AABB Other { get; }

        public AABBIntersectionResult(Vector3 position, Vector3 movement, AABBFace face, AABB other)
        {
            Position = position;
            Movement = movement;
            Face = face;
            Other = other;

            Normal = Face switch
            {
                AABBFace.Left => -Vector3.UnitX,
                AABBFace.Right => Vector3.UnitX,
                AABBFace.Bottom => -Vector3.UnitY,
                AABBFace.Top => Vector3.UnitY,
                AABBFace.Back => -Vector3.UnitZ,
                AABBFace.Front => Vector3.UnitZ,
                _ => Vector3.One.Normalized()
            };
        }
    }
}
