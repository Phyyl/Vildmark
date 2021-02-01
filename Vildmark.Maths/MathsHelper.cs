using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Maths
{
    public static class MathsHelper
    {
        public const float PiOver12 = MathHelper.Pi / 12;
        public const float PiOver8 = MathHelper.Pi / 8;
        public const float PiOver6 = MathHelper.Pi / 6;
        public const float PiOver4 = MathHelper.Pi / 4;
        public const float PiOver3 = MathHelper.Pi / 3;
        public const float PiOver2 = MathHelper.Pi / 2;
        public const float Pi = MathHelper.Pi;
        public const float Tau = MathHelper.TwoPi;

        public static float Sin(float value) => (float)Math.Sin(value);
        public static float Sinh(float value) => (float)Math.Sinh(value);
        public static float Asin(float value) => (float)Math.Asin(value);
        public static float Asinh(float value) => (float)Math.Asinh(value);
        public static float Cos(float value) => (float)Math.Cos(value);
        public static float Cosh(float value) => (float)Math.Cosh(value);
        public static float Acos(float value) => (float)Math.Acos(value);
        public static float Acosh(float value) => (float)Math.Acosh(value);
        public static float Tan(float value) => (float)Math.Tan(value);
        public static float Tanh(float value) => (float)Math.Tanh(value);
        public static float Atan(float value) => (float)Math.Atan(value);
        public static float Atanh(float value) => (float)Math.Atanh(value);
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);

        public static float Round(float value, int digits = 0) => (float)Math.Round(value, digits);
        public static float Floor(float value) => (float)Math.Floor(value);
        public static float Ceiling(float value) => (float)Math.Ceiling(value);

        public static float Min(float a, float b) => a < b ? a : b;
        public static float Max(float a, float b) => a > b ? a : b;

        public static Vector2 Min(Vector2 a, Vector2 b) => new Vector2(Min(a.X, b.X), Min(a.Y, b.Y));
        public static Vector3 Min(Vector3 a, Vector3 b) => new Vector3(Min(a.X, b.X), Min(a.Y, b.Y), Min(a.Z, b.Z));
        public static Vector4 Min(Vector4 a, Vector4 b) => new Vector4(Min(a.X, b.X), Min(a.Y, b.Y), Min(a.Z, b.Z), Min(a.W, b.W));

        public static Vector2 Max(Vector2 a, Vector2 b) => new Vector2(Max(a.X, b.X), Max(a.Y, b.Y));
        public static Vector3 Max(Vector3 a, Vector3 b) => new Vector3(Max(a.X, b.X), Max(a.Y, b.Y), Max(a.Z, b.Z));
        public static Vector4 Max(Vector4 a, Vector4 b) => new Vector4(Max(a.X, b.X), Max(a.Y, b.Y), Max(a.Z, b.Z), Max(a.W, b.W));

        public static int Mod(int x, int mod) => ((x % mod) + mod) % mod;
        public static float Mod(float x, float mod) => ((x % mod) + mod) % mod;
        public static double Mod(double x, double mod) => ((x % mod) + mod) % mod;

        public static void ConvertTo3DIndex(int index, int width, int height, int depth, out int x, out int y, out int z)
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
