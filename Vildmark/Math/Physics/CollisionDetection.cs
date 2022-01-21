using OpenTK.Mathematics;
using System;

namespace Vildmark.Maths.Physics
{
    public static class CollisionDetection
    {
        public static AABB3DIntersectionResult? IntersectMovingAABBToAABB(AABB3D a, Vector3 movement, AABB3D b)
        {
            LineSegment3D segment = new(a.Center, a.Center + movement);
            AABB3D other = b.Inflate(a.Size);

            return IntersectLineSegmentToAABB(segment, other);
        }
        public static AABB2DIntersectionResult? IntersectMovingAABBToAABB(AABB2D a, Vector2 movement, AABB2D b)
        {
            LineSegment2D segment = new(a.Center, a.Center + movement);
            AABB2D other = b.Inflate(a.Size);

            return IntersectLineSegmentToAABB(segment, other);
        }

        public static AABB3DIntersectionResult<T>? IntersectMovingAABBToAABB<T>(AABB3D a, Vector3 movement, AABB3D b, T target)
        {
            AABB3DIntersectionResult? result = IntersectMovingAABBToAABB(a, movement, b);

            if (result is not null)
            {
                return new AABB3DIntersectionResult<T>(result.Position, result.Movement, result.Face, result.Other, target);
            }

            return null;
        }
        public static AABB2DIntersectionResult<T>? IntersectMovingAABBToAABB<T>(AABB2D a, Vector2 movement, AABB2D b, T target)
        {
            AABB2DIntersectionResult? result = IntersectMovingAABBToAABB(a, movement, b);

            if (result is not null)
            {
                return new AABB2DIntersectionResult<T>(result.Position, result.Movement, result.Face, result.Other, target);
            }

            return null;
        }

        public static AABB3DIntersectionResult? IntersectLineSegmentToAABB(LineSegment3D line, AABB3D box)
        {
            float tMax = 1;
            float tMin = 0;
            Vector3 direction = line.Movement;

            for (int i = 0; i < 3; i++)
            {
                if (Math.Abs(direction[i]) < float.Epsilon)
                {
                    if (line.Start[i] <= box.Min[i] || line.Start[i] >= box.Max[i])
                    {
                        return default;
                    }
                }
                else
                {
                    float ood = 1 / direction[i];
                    float t1 = (box.Min[i] - line.Start[i]) * ood;
                    float t2 = (box.Max[i] - line.Start[i]) * ood;

                    if (t1 > t2)
                    {
                        (t1, t2) = (t2, t1);
                    }

                    tMin = Math.Max(tMin, t1);
                    tMax = Math.Min(tMax, t2);

                    if (tMin > tMax)
                    {
                        return default;
                    }
                }
            }

            Vector3 movement = direction * tMin;
            Vector3 position = line.Start + movement;

            AABBFace GetFace()
            {
                if (position.X == box.Left) return AABBFace.Left;
                if (position.X == box.Right) return AABBFace.Right;
                if (position.Y == box.Bottom) return AABBFace.Bottom;
                if (position.Y == box.Top) return AABBFace.Top;
                if (position.Z == box.Back) return AABBFace.Back;
                if (position.Z == box.Front) return AABBFace.Front;
                return AABBFace.None;
            }

            return new AABB3DIntersectionResult(position, movement, GetFace(), box);
        }
        public static AABB2DIntersectionResult? IntersectLineSegmentToAABB(LineSegment2D line, AABB2D box)
        {
            float tMax = 1;
            float tMin = float.Epsilon;
            Vector2 direction = line.Movement;

            for (int i = 0; i < 2; i++)
            {
                if (Math.Abs(direction[i]) < float.Epsilon)
                {
                    if (line.Start[i] <= box.Min[i] || line.Start[i] >= box.Max[i])
                    {
                        return default;
                    }
                }
                else
                {
                    float ood = 1 / direction[i];
                    float t1 = (box.Min[i] - line.Start[i]) * ood;
                    float t2 = (box.Max[i] - line.Start[i]) * ood;

                    if (t1 > t2)
                    {
                        (t1, t2) = (t2, t1);
                    }

                    tMin = Math.Max(tMin, t1);
                    tMax = Math.Min(tMax, t2);

                    if (tMin > tMax)
                    {
                        return default;
                    }
                }
            }

            Vector2 movement = direction * tMin;
            Vector2 position = line.Start + movement;

            AABBFace GetFace()
            {
                if (position.X == box.Left) return AABBFace.Left;
                if (position.X == box.Right) return AABBFace.Right;
                if (position.Y == box.Bottom) return AABBFace.Bottom;
                if (position.Y == box.Top) return AABBFace.Top;

                return AABBFace.None;
            }

            return new AABB2DIntersectionResult(position, movement, GetFace(), box);
        }
    }
}
