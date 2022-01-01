using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics
{
    internal delegate void UniformSetter<T>(int location, T value, int index);
    internal static class StructTypeInfo
    {
        static StructTypeInfo()
        {
            Storage<float>.UniformSetter = (l, v, i) => GL.Uniform1(l, v);

            Storage<int>.UniformSetter = (l, v, i) => GL.Uniform1(l, v);
            Storage<int>.AttribType = VertexAttribPointerType.Int;

            Storage<uint>.UniformSetter = (l, v, i) => GL.Uniform1(l, v);
            Storage<uint>.AttribType = VertexAttribPointerType.UnsignedInt;

            Storage<Vector2>.UniformSetter = (l, v, i) => GL.Uniform2(l, v);
            Storage<Vector2>.AttribSize = 2;

            Storage<Vector3>.UniformSetter = (l, v, i) => GL.Uniform3(l, v);
            Storage<Vector3>.AttribSize = 3;

            Storage<Vector4>.UniformSetter = (l, v, i) => GL.Uniform4(l, v);
            Storage<Vector4>.AttribSize = 4;
            Storage<Matrix4>.UniformSetter = (l, v, i) => GL.UniformMatrix4(l, false, ref v);

            Storage<Color4>.UniformSetter = (l, v, i) => GL.Uniform4(l, v);
            Storage<Color4>.AttribSize = 4;

            Storage<GLTexture2D>.UniformSetter = (l, v, i) =>
            {
                v.Bind(i);
                GL.Uniform1(l, i);
            };

            Storage<Texture2D>.UniformSetter = (l, v, i) => SetUniform(l, v.GLTexture);
            Storage<Texture2D[]>.UniformSetter = (l, v, i) =>
            {
                for (int j = 0; j < v.Length; j++)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + j);
                    SetUniform(l + i + j, v[j], j);
                }
            };
        }

        public static bool SetUniform<T>(int location, T value, int index = 0)
        {
            if (Storage<T>.UniformSetter is { } setter)
            {
                setter(location, value, index);
                return true;
            }

            return false;
        }

        public static int GetAttribSize<T>() => Storage<T>.AttribSize;
        public static VertexAttribPointerType GetAttribType<T>() => Storage<T>.AttribType;

        private static class Storage<T>
        {
            public static UniformSetter<T>? UniformSetter;
            public static int AttribSize = 1;
            public static VertexAttribPointerType AttribType = VertexAttribPointerType.Float;
        }
    }
}
