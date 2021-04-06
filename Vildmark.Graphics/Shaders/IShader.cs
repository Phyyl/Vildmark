using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
    public interface IShader
    {
        void Use();
    }

    public interface IColorShader : IShader
    {
        Attrib<Vector4> Color { get; }
    }

    public interface ITexCoordShader : IShader
    {
        Attrib<Vector2> TexCoord { get; }
    }

    public interface IPositionShader : IShader
    {
        Attrib<Vector3> Position { get; }
    }

    public interface IPosition2Shader : IShader
    {
        Attrib<Vector2> Position { get; }
    }

    public interface INormalShader : IShader
    {
        Attrib<Vector3> Normal { get; }
    }

    public interface IModelMatrixShader : IShader
    {
        Uniform<Matrix4> ModelMatrix { get; }
    }

    public interface IViewMatrixShader : IShader
    {
        Uniform<Matrix4> ViewMatrix { get; }
    }

    public interface IProjectionMatrixShader : IShader
    {
        Uniform<Matrix4> ProjectionMatrix { get; }
    }

    public interface INormalMatrixShader : IShader
    {
        Uniform<Matrix4> NormalMatrix { get; }
    }

    public interface IModelViewProjectionMatrixShader : IProjectionMatrixShader, IViewMatrixShader, IModelMatrixShader
    {
    }

    public interface ITextureShader : IShader
    {
        Uniform<GLTexture2D> Texture { get; }
    }

    public interface ISourceRectShader : IShader
    {
        Uniform<Vector4> SourceRect { get; }
    }

    public interface ITexturesShader : IShader
    {
        int MaxTextures { get; }

        IndexedUniform<GLTexture2D> Textures { get; }
    }

    public interface ITextureIndexShader : IShader
    {
        Attrib<int> TextureIndex { get; }
    }

    public interface ITintShader : IShader
    {
        Uniform<Vector4> Tint { get; }
    }

    public interface IOffsetShader
    {
        Uniform<Vector3> Offset { get; }
    }

    public interface IVertexShader : IPositionShader, IColorShader, ITexCoordShader, INormalShader
    {
    }
}
