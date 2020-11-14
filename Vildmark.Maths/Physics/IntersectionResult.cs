using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Maths.Physics
{
    public struct IntersectionResult
    {
        public bool Intersects;
        public float Distance;
        public Vector3 Position;
        public Vector3 Normal;

        public IntersectionResult(bool intersects, float distance, Vector3 position, Vector3 normal)
        {
            Intersects = intersects;
            Distance = distance;
            Position = position;
            Normal = normal;
        }
    }
}
