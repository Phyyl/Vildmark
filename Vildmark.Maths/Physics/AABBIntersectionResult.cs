using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Maths.Physics
{
    public class AABBIntersectionResult
    {
        public Vector3 Position { get; }
        public Vector3 Movement { get; }
        public AABBFace Face { get; }
        public AABB Other { get; }

        public AABBIntersectionResult(Vector3 position, Vector3 movement, AABBFace face, AABB other)
        {
            Position = position;
            Movement = movement;
            Face = face;
            Other = other;
        }
    }
}
