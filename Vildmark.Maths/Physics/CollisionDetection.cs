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
        public static bool GetMovingAABBToAABBDistance(AABB a, Vector3 movement, AABB b, out float distance, out Vector3 intersection)
        {
            return GetRayToAABBIntersection(new Ray(a.Center, movement), b.Inflate(a.Size), out distance, out intersection);
        }

        public static bool GetRayToAABBIntersection(Ray ray, AABB box, out float distance, out Vector3 intersection)
        {
            float tMax = float.PositiveInfinity;

            distance = default;
            intersection = default;

            for (int i = 0; i < 3; i++)
            {
                if (Math.Abs(ray.Direction[i]) < float.Epsilon)
                {
                    if (ray.Start[i] < box.Min[i] || ray.Start[i] > box.Max[i])
                    {
                        return false;
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

                    distance = Math.Max(distance, t1);
                    tMax = Math.Min(tMax, t2);

                    if (distance > tMax)
                    {
                        return false;
                    }
                }
            }

            intersection = ray.Start + ray.Direction * distance;

            return true;
        }
    }
}
