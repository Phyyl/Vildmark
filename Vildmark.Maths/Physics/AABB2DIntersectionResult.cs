using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Maths.Physics
{
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
}
