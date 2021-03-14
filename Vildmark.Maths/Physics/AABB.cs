using OpenTK.Mathematics;
using System;

namespace Vildmark.Maths.Physics
{
    public struct AABB
    {
        public Vector3 Position;
        public Vector3 Size;

        public Vector3 Min => Position;
        public Vector3 Max => Position + Size;
        public Vector3 Center => Position + Size / 2;

        public float Left => Position.X;
        public float Right => Position.X + Size.X;
        public float Bottom => Position.Y;
        public float Top => Position.Y + Size.Y;
        public float Back => Position.Z;
        public float Front => Position.Z + Size.Z;

        public AABB(Vector3 position, Vector3 size)
        {
            Position = position;
            Size = size;
        }

        public AABB Inflate(Vector3 size)
        {
            return new AABB(Position - size / 2, Size + size);
        }

        public AABB Offset(Vector3 offset)
        {
            return new AABB(Position + offset, Size);
        }

        public AABB Join(AABB b)
        {
            Vector3 position = new(Math.Min(Left,b.Left), Math.Min(Bottom, b.Bottom), Math.Min(Back, b.Back));
            Vector3 size = new Vector3(Math.Max(Right, b.Right), Math.Max(Top, b.Top), Math.Max(Front, b.Front)) - position;

            return new AABB(position, size);
        }

        public bool Intersects(AABB b)
        {
            return Right > b.Left &&
                Left < b.Right &&
                Front > b.Back &&
                Back < b.Front &&
                Top > b.Bottom &&
                Bottom < b.Top;
        }

        public bool Contains(Vector3 point)
        {
            return point.X > Left
                && point.X < Right
                && point.Y > Bottom
                && point.Y < Top
                && point.Z > Back
                && point.Z < Front;
        }

        public override string ToString()
        {
            return $"{Position}, {Size}";
        }
    }
}
