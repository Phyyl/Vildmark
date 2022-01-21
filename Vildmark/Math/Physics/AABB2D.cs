using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Maths.Physics
{
    public struct AABB2D
    {
        public Vector2 Position;
        public Vector2 Size;

        public Vector2 Min => Position;
        public Vector2 Max => Position + Size;
        public Vector2 Center => Position + Size / 2;

        public float Left => Position.X;
        public float Right => Position.X + Size.X;
        public float Top => Position.Y;
        public float Bottom => Position.Y + Size.Y;

        public AABB2D(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public AABB2D Inflate(Vector2 size)
        {
            return new AABB2D(Position - size / 2, Size + size);
        }

        public AABB2D Offset(Vector2 offset)
        {
            return new AABB2D(Position + offset, Size);
        }

        public AABB2D Join(AABB3D b)
        {
            Vector2 position = new(Math.Min(Left, b.Left), Math.Min(Top, b.Top));
            Vector2 size = new Vector2(Math.Max(Right, b.Right), Math.Max(Bottom, b.Bottom)) - position;

            return new AABB2D(position, size);
        }

        public bool Intersects(AABB2D b)
        {
            return !(Right > b.Left ||
                Left < b.Right ||
                Top > b.Bottom ||
                Bottom < b.Top);
        }

        public bool Contains(Vector2 point)
        {
            return point.X >= Left
                && point.X <= Right
                && point.Y >= Top
                && point.Y <= Bottom;
        }

        public override string ToString()
        {
            return $"{Position}, {Size}";
        }
    }
}
