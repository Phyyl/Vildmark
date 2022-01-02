using OpenTK.Mathematics;
using System;

namespace Vildmark.Maths
{
    public static class MathsHelper
    {
        public const float GoldenRatio = 1.618033988749894848204586834365638117720309179805762862135448622705260462818902449707207204f;

        public const float Qau = MathF.PI / 2f;
        public const float Pau = MathF.PI * 1.5f;
        public const float Dau = MathF.Tau * 2;
        public const float Gau = MathF.Tau * GoldenRatio;

        public static Vector2 Min(Vector2 a, Vector2 b) => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y));
        public static Vector3 Min(Vector3 a, Vector3 b) => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y), MathF.Min(a.Z, b.Z));
        public static Vector4 Min(Vector4 a, Vector4 b) => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y), MathF.Min(a.Z, b.Z), MathF.Min(a.W, b.W));

        public static Vector2 Max(Vector2 a, Vector2 b) => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y));
        public static Vector3 Max(Vector3 a, Vector3 b) => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y), MathF.Max(a.Z, b.Z));
        public static Vector4 Max(Vector4 a, Vector4 b) => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y), MathF.Max(a.Z, b.Z), MathF.Max(a.W, b.W));

        public static int Mod(int x, int mod) => ((x % mod) + mod) % mod;
        public static float Mod(float x, float mod) => ((x % mod) + mod) % mod;
        public static double Mod(double x, double mod) => ((x % mod) + mod) % mod;

        public static void ConvertTo3DIndex(int index, int height, int depth, out int x, out int y, out int z)
        {
            x = index / (height * depth);
            y = (index / depth) % height;
            z = index % depth;
        }

        public static Vector2 VectorFromAngle(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
