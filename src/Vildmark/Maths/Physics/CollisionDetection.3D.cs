using BoxType = OpenTK.Mathematics.Box3;
using VectorType = OpenTK.Mathematics.Vector3;
using BoxInterfaceType = Vildmark.Maths.Physics.IBox3;
using IntersectionType = Vildmark.Maths.Physics.Intersection3;
using GenericIntersectionType = Vildmark.Maths.Physics.Intersection3<Vildmark.Maths.Physics.IBox3>;
using LineSegmentType = Vildmark.Maths.Physics.LineSegment3;
using FaceType = Vildmark.Maths.Physics.Face3;

namespace Vildmark.Maths.Physics;

public static partial class CollisionDetection
{
    public static IntersectionType? MovingBoxToBoxes(BoxType a, VectorType movement, params BoxType[] boxes) => boxes
            .Select(b => MovingBoxToBox(a, movement, b))
            .NotNull()
            .OrderBy(i => (i.HitPosition - a.Center).LengthSquared)
            .FirstOrDefault();

    public static GenericIntersectionType? MovingBoxToBoxes(BoxType a, VectorType movement, params BoxInterfaceType[] boxes) => boxes
            .Select(b => MovingBoxToBox(a, movement, b))
            .NotNull()
            .OrderBy(i => (i.HitPosition - a.Center).LengthSquared)
            .FirstOrDefault();

    public static IntersectionType? MovingBoxToBox(BoxType a, VectorType movement, BoxType b)
    {
        return LineSegmentToBox(new(a.Center, a.Center + movement), b.ActuallyInflated(a.HalfSize));
    }

    public static GenericIntersectionType? MovingBoxToBox<T>(BoxType a, VectorType movement, T b) where T : BoxInterfaceType
    {
        if (LineSegmentToBox(new(a.Center, a.Center + movement), b.Box.ActuallyInflated(a.HalfSize)) is { } intersection)
        {
            return new(intersection, b);
        }
        return default;

    }

    public static IntersectionType? LineSegmentToBox(LineSegmentType line, BoxType box)
    {
        if (box.Contains(line.Start) || line.Delta.LengthSquared < float.Epsilon)
        {
            return default;
        }

        float tMax = 1;
        float tMin = float.Epsilon;
        VectorType delta = line.Delta;
        IntersectionType? intersection = null;

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

                    VectorType pos = line.Start + delta * tMin;
                    pos[i] = t1.min ? box.Min[i] : box.Max[i];

                    intersection = new(pos, (FaceType)(1 << (i * 2 + (t1.min ? 0 : 1))));
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
