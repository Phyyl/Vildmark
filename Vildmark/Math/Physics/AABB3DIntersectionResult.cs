using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics
{
    public class AABB3DIntersectionResult
    {
        public Vector3 Position { get; }
        public Vector3 Movement { get; }
        public Vector3 Normal { get; }
        public AABBFace Face { get; }
        public AABB3D Other { get; }

        public AABB3DIntersectionResult(Vector3 position, Vector3 movement, AABBFace face, AABB3D other)
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

    public class AABB3DIntersectionResult<T> : AABB3DIntersectionResult
    {
        public T Target { get; }

        public AABB3DIntersectionResult(Vector3 position, Vector3 movement, AABBFace face, AABB3D other, T target)
            : base(position, movement, face, other)
        {
            Target = target;
        }
    }
}
