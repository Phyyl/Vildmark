using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public class AABB2DIntersectionResult
{
    public Vector2 Position { get; }
    public Vector2 Movement { get; }
    public Vector2 Normal { get; }
    public AABBFace Face { get; }
    public AABB2D Other { get; }

    public AABB2DIntersectionResult(Vector2 position, Vector2 movement, AABBFace face, AABB2D other)
    {
        Position = position;
        Movement = movement;
        Face = face;
        Other = other;

        Normal = Face switch
        {
            AABBFace.Left => -Vector2.UnitX,
            AABBFace.Right => Vector2.UnitX,
            AABBFace.Bottom => -Vector2.UnitY,
            AABBFace.Top => Vector2.UnitY,
            _ => Vector2.One.Normalized()
        };
    }
}

public class AABB2DIntersectionResult<T> : AABB2DIntersectionResult
{
    public T Target { get; }

    public AABB2DIntersectionResult(Vector2 position, Vector2 movement, AABBFace face, AABB2D other, T target)
        : base(position, movement, face, other)
    {
        Target = target;
    }
}
