using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace Vildmark.Maths.Physics
{
    public static class CollisionDetection
    {
        public static AABBIntersectionResult IntersectMovingAABBToAABB(AABB a, Vector3 movement, AABB b)
        {
            LineSegment segment = new(a.Center, a.Center + movement);
            AABB other = b.Inflate(a.Size);

            return IntersectLineSegmentToAABB(segment, other);
        }

        public static AABBIntersectionResult IntersectLineSegmentToAABB(LineSegment line, AABB box)
        {
            float tMax = 1;
            float tMin = 0;
            Vector3 direction = line.Movement;

            for (int i = 0; i < 3; i++)
            {
                if (Math.Abs(direction[i]) < float.Epsilon)
                {
                    if (line.Start[i] < box.Min[i] || line.Start[i] > box.Max[i])
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

            return new AABBIntersectionResult(position, movement, GetFace(), box);
        }
    }
}
