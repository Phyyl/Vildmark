using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Logging;

namespace Vildmark.Graphics.Shaders
{
    public abstract class Uniform : ShaderVariable
    {
        static Uniform()
        {
            Setter<bool>.Action = Set;

            Setter<byte>.Action = Set;
            Setter<sbyte>.Action = Set;

            Setter<short>.Action = Set;
            Setter<ushort>.Action = Set;

            Setter<int>.Action = Set;
            Setter<uint>.Action = Set;


            Setter<long>.Action = Set;
            Setter<ulong>.Action = Set;


            Setter<float>.Action = Set;
            Setter<double>.Action = Set;

            Setter<Vector2>.Action = Set;
            Setter<Vector3>.Action = Set;
            Setter<Vector4>.Action = Set;

            Setter<Matrix2>.Action = Set;
            Setter<Matrix2d>.Action = Set;
            Setter<Matrix2x3>.Action = Set;
            Setter<Matrix2x3d>.Action = Set;
            Setter<Matrix2x4>.Action = Set;
            Setter<Matrix2x4d>.Action = Set;

            Setter<Matrix3>.Action = Set;
            Setter<Matrix3d>.Action = Set;
            Setter<Matrix3x2>.Action = Set;
            Setter<Matrix3x2d>.Action = Set;
            Setter<Matrix3x4>.Action = Set;
            Setter<Matrix3x4d>.Action = Set;

            Setter<Matrix4>.Action = Set;
            Setter<Matrix4d>.Action = Set;
            Setter<Matrix4x2>.Action = Set;
            Setter<Matrix4x2d>.Action = Set;
            Setter<Matrix4x3>.Action = Set;
            Setter<Matrix4x3d>.Action = Set;

            Setter<GLTexture2D>.Action = Set;
            IndexedSetter<GLTexture2D>.Action = Set;
        }

        protected Uniform(string name)
            : base(name)
        {
        }

        private static void Set(int location, bool value) => GL.Uniform1(location, value ? 1 : 0);

        private static void Set(int location, byte value) => GL.Uniform1(location, value);
        private static void Set(int location, sbyte value) => GL.Uniform1(location, value);

        private static void Set(int location, short value) => GL.Uniform1(location, value);
        private static void Set(int location, ushort value) => GL.Uniform1(location, value);

        private static void Set(int location, int value) => GL.Uniform1(location, value);
        private static void Set(int location, uint value) => GL.Uniform1(location, value);

        private static void Set(int location, long value) => GL.Uniform1(location, value);
        private static void Set(int location, ulong value) => GL.Uniform1(location, value);

        private static void Set(int location, float value) => GL.Uniform1(location, value);
        private static void Set(int location, double value) => GL.Uniform1(location, value);

        private static void Set(int location, Vector2 value) => GL.Uniform2(location, value);
        private static void Set(int location, Vector3 value) => GL.Uniform3(location, value);
        private static void Set(int location, Vector4 value) => GL.Uniform4(location, value);

        private static void Set(int location, Matrix2 value) => GL.UniformMatrix2(location, false, ref value);
        private static void Set(int location, Matrix2d value) => GL.UniformMatrix2(location, false, ref value);
        private static void Set(int location, Matrix2x3 value) => GL.UniformMatrix2x3(location, false, ref value);
        private static void Set(int location, Matrix2x3d value) => GL.UniformMatrix2x3(location, false, ref value);
        private static void Set(int location, Matrix2x4 value) => GL.UniformMatrix2x4(location, false, ref value);
        private static void Set(int location, Matrix2x4d value) => GL.UniformMatrix2x4(location, false, ref value);

        private static void Set(int location, Matrix3 value) => GL.UniformMatrix3(location, false, ref value);
        private static void Set(int location, Matrix3d value) => GL.UniformMatrix3(location, false, ref value);
        private static void Set(int location, Matrix3x2 value) => GL.UniformMatrix3x2(location, false, ref value);
        private static void Set(int location, Matrix3x2d value) => GL.UniformMatrix3x2(location, false, ref value);
        private static void Set(int location, Matrix3x4 value) => GL.UniformMatrix3x4(location, false, ref value);
        private static void Set(int location, Matrix3x4d value) => GL.UniformMatrix3x4(location, false, ref value);

        private static void Set(int location, Matrix4 value) => GL.UniformMatrix4(location, false, ref value);
        private static void Set(int location, Matrix4d value) => GL.UniformMatrix4(location, false, ref value);
        private static void Set(int location, Matrix4x2 value) => GL.UniformMatrix4x2(location, false, ref value);
        private static void Set(int location, Matrix4x2d value) => GL.UniformMatrix4x2(location, false, ref value);
        private static void Set(int location, Matrix4x3 value) => GL.UniformMatrix4x3(location, false, ref value);
        private static void Set(int location, Matrix4x3d value) => GL.UniformMatrix4x3(location, false, ref value);

        private static void Set(int location, GLTexture2D value) => Set(location, 0, value);
        private static void Set(int location, int index, GLTexture2D value)
        {
            if (value is null)
            {
                return;
            }

            value.Bind(index);
            GL.Uniform1(location, index);
        }

        protected static void SetValue<T>(int location, T value) => Setter<T>.Action?.Invoke(location, value);

        protected static void SetIndexedValue<T>(int location, int index, T value) => IndexedSetter<T>.Action?.Invoke(location, index, value);

        protected static class Setter<T>
        {
            public delegate void SetterAction(int location, T value);

            public static SetterAction Action { get; set; }
        }

        protected static class IndexedSetter<T>
        {
            public delegate void IndexedSetterAction(int location, int index, T value);

            public static IndexedSetterAction Action { get; set; }
        }
    }

    public class Uniform<T> : Uniform
    {
        public Uniform(string name)
            : base(name)
        {
            if (Setter<T>.Action == null)
            {
                Service<ILogger>.Instance?.Warning($"No setter action has been set for uniform type {typeof(T).Name}");
            }
        }

        public void SetValue(T value)
        {
            if (!Defined || !Enabled)
            {
                return;
            }

            SetValue(Location, value);
        }
    }

    public class IndexedUniform<T> : Uniform<T>
    {
        public IndexedUniform(string name)
            : base(name)
        {
            if (IndexedSetter<T>.Action == null)
            {
                Service<ILogger>.Instance?.Warning($"No indexed setter action has been set for uniform type {typeof(T).Name}");
            }
        }

        public void SetValue(T value, int index)
        {
            if (!Defined || !Enabled)
            {
                return;
            }

            SetIndexedValue(Location, index, value);
        }
    }
}
