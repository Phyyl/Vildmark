using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Textures;
using Vildmark.Logging;

namespace Vildmark.Graphics;

internal delegate void UniformSetter<T>(int location, T value, int index);
internal static class StaticTypeInfo
{
    static StaticTypeInfo()
    {
        Storage<float>.UniformSetter = (l, v, i) => GL.Uniform1f(l, v);

        Storage<int>.UniformSetter = (l, v, i) => GL.Uniform1i(l, v);
        Storage<int>.AttribType = VertexAttribPointerType.Int;

        Storage<uint>.UniformSetter = (l, v, i) => GL.Uniform1ui(l, v);
        Storage<uint>.AttribType = VertexAttribPointerType.UnsignedInt;

        Storage<Vector2>.UniformSetter = (l, v, i) => GL.Uniform2f(l, 1, ref v);
        Storage<Vector2>.AttribSize = 2;

        Storage<Vector3>.UniformSetter = (l, v, i) => GL.Uniform3f(l, 1, ref v);
        Storage<Vector3>.AttribSize = 3;

        Storage<Vector4>.UniformSetter = (l, v, i) => GL.Uniform4f(l, 1, ref v);
        Storage<Vector4>.AttribSize = 4;

        Storage<Box2>.UniformSetter = (l, v, i) => SetUniform(l, v.ToVector4(), i);
        Storage<Box2>.AttribSize = 4;

        Storage<Matrix4>.UniformSetter = (l, v, i) => GL.UniformMatrix4f(l, 1, false, ref v);
        Storage<Transform?>.UniformSetter = (l, v, i) => SetUniform(l, v?.Matrix ?? Matrix4.Identity, i);

        Storage<Color4<Rgba>>.UniformSetter = (l, v, i) => GL.Uniform4f(l, v.X, v.Y, v.Z, v.W);
        Storage<Color4<Rgba>>.AttribSize = 4;

        Storage<GLTexture2D>.UniformSetter = (l, v, i) =>
        {
            v.Bind(i);
            GL.Uniform1i(l, i);
        };

        Storage<GLTexture2D[]>.UniformSetter = (l, v, i) =>
        {
            for (int j = 0; j < v.Length; j++)
            {
                SetUniform(l + i + j, v[i], j);
            }
        };

        Storage<Texture2D>.UniformSetter = (l, v, i) => SetUniform(l, v.GLTexture, i);
        Storage<Texture2D[]>.UniformSetter = (l, v, i) =>
        {
            for (int j = 0; j < v.Length; j++)
            {
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
        else
        {
            Logger.Warning($"Uniform setter for type {typeof(T).Name} not found");
        }

        return false;
    }

    public static int GetAttribSize<T>() => Storage<T>.AttribSize;
    public static VertexAttribPointerType GetAttribType<T>() => Storage<T>.AttribType;

    public static bool TryGetAttribSize(Type type, out int size)
    {
        if (typeof(Storage<>).MakeGenericType(type).GetField(nameof(Storage<float>.AttribSize))?.GetValue(null) is int i)
        {
            size = i;
            return true;
        }

        size = 0;
        return false;
    }

    public static bool TryGetAttribType(Type type, out VertexAttribPointerType attribType)
    {
        if (typeof(Storage<>).MakeGenericType(type).GetField(nameof(Storage<float>.AttribType))?.GetValue(null) is VertexAttribPointerType t)
        {
            attribType = t;
            return true;
        }

        attribType = VertexAttribPointerType.Float;
        return false;
    }

    private static class Storage<T>
    {
        public static UniformSetter<T>? UniformSetter;
        public static int AttribSize = 1;
        public static VertexAttribPointerType AttribType = VertexAttribPointerType.Float;
    }
}
