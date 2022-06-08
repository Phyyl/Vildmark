using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public record Intersection2(Vector2 HitPosition, Face2 Face);
public record Intersection2<T> : Intersection2 where T : IBox2
{
    public Intersection2(Intersection2 other, T value)
        : base(other)
    {
        Value = value;
    }

    public T Value { get; }
}

public record Intersection3(Vector3 HitPosition, Face3 Face);
public record Intersection3<T> : Intersection3 where T : IBox3
{
    public Intersection3(Intersection3 other, T value)
        : base(other)
    {
        Value = value;
    }

    public T Value { get; }
}

