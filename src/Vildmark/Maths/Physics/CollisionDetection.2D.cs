using OpenTK.Mathematics;

namespace Vildmark.Maths.Physics;

public static partial class CollisionDetection
{
    public static Intersection2? MovingBoxToBoxes(Box2 a, Vector2 movement, params Box2[] boxes) => boxes
            .Select(b => MovingBoxToBox(a, movement, b))
            .NotNull()
            .OrderBy(i => (i.HitPosition - a.Center).LengthSquared)
            .FirstOrDefault();

    public static Intersection2<T>? MovingBoxToBoxes<T>(Box2 a, Vector2 movement, params T[] boxes) where T : IBox2 => boxes
            .Select(b => MovingBoxToBox(a, movement, b))
            .NotNull()
            .OrderBy(i => (i.HitPosition - a.Center).LengthSquared)
            .FirstOrDefault();

    public static Intersection2? MovingBoxToBox(Box2 a, Vector2 movement, Box2 b)
    {
        return LineSegmentToBox(new(a.Center, a.Center + movement), b.ActuallyInflated(a.HalfSize));
    }

    public static Intersection2<T>? MovingBoxToBox<T>(Box2 a, Vector2 movement, T b) where T : IBox2
    {
        if (LineSegmentToBox(new(a.Center, a.Center + movement), b.Box.ActuallyInflated(a.HalfSize)) is { } intersection)
        {
            return new(intersection, b);
        }
        return default;

    }

    public static Intersection2? LineSegmentToBox(LineSegment2 line, Box2 box)
    {
        if (box.Contains(line.Start) || line.Delta.LengthSquared < float.Epsilon)
        {
            return default;
        }

        float tMax = 1;
        float tMin = float.Epsilon;
        Vector2 delta = line.Delta;
        Intersection2? intersection = null;

        for (int i = 0; i < 2; i++)
        {
            if (Math.Abs(delta[i]) < float.Epsilon)
            {
                if (line.Start[i] <= box.Min[i] || line.Start[i] >= box.Max[i])
                {
                    return default;
                }
            }
            else
            {
                float ood = 1 / delta[i];
                (float t, bool min) t1 = ((box.Min[i] - line.Start[i]) * ood, true);
                (float t, bool min) t2 = ((box.Max[i] - line.Start[i]) * ood, false);

                if (t1.t > t2.t)
                {
                    (t1, t2) = (t2, t1);
                }

                if (t1.t > tMin)
                {
                    tMin = t1.t;

                    Vector2 pos = line.Start + delta * tMin;
                    pos[i] = t1.min ? box.Min[i] : box.Max[i];

                    intersection = new(pos, (Face2)(1 << (i * 2 + (t1.min ? 0 : 1))));
                }

                tMax = Math.Min(tMax, t2.t);

                if (tMin > tMax)
                {
                    return default;
                }
            }
        }

        return intersection;
    }
}
