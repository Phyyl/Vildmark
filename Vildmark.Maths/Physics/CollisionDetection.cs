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
        public static IntersectionResult IntersectMovingAABBToAABB(AABB a, Vector3 movement, AABB b)
        {
            return IntersectRayToAABB(new Ray(a.Center, movement), b.Inflate(a.Size));
        }

        public static IntersectionResult IntersectRayToAABB(Ray ray, AABB box)
        {
            IntersectionResult result = new IntersectionResult(false, 0, ray.Start, -ray.Direction.Normalized());
            float tMax = float.PositiveInfinity;

            for (int i = 0; i < 3; i++)
            {
                if (Math.Abs(ray.Direction[i]) < float.Epsilon)
                {
                    if (ray.Start[i] < box.Min[i] || ray.Start[i] > box.Max[i])
                    {
                        return default;
                    }
                }
                else
                {
                    float ood = 1 / ray.Direction[i];
                    float t1 = (box.Min[i] - ray.Start[i]) * ood;
                    float t2 = (box.Max[i] - ray.Start[i]) * ood;

                    if (t1 > t2)
                    {
                        (t1, t2) = (t2, t1);
                    }

                    if (t1 > result.Distance)
                    {
                        result.Distance = t1;
                        result.Normal = default;
                        result.Normal[i] = -Math.Sign(ray.Direction[i]);
                    }

                    tMax = Math.Min(tMax, t2);

                    if (result.Distance > tMax)
                    {
                        return default;
                    }
                }
            }

            result.Position = ray.Start + ray.Direction * result.Distance;
            result.Intersects = result.Distance < 1;

            return result;
        }
    }
}
