using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Textures;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders;

public record TexturedMaterial(Texture2D Texture, Color4 Tint)
{
    public static implicit operator TexturedMaterial(Color4 tint) => new(Texture2D.WhitePixel, tint);
    public static implicit operator TexturedMaterial(Texture2D texture) => new(texture, Color4.White);
    public static implicit operator TexturedMaterial(GLTexture2D texture) => new(texture, Color4.White);
}

public class TexturedShader : Shader<Vertex, TexturedMaterial>
{
    public Attrib<Vector3> PositionAttrib { get; } = new("vert_position");
    public Attrib<Vector4> ColorAttrib { get; } = new("vert_color");
    public Attrib<Vector2> TexCoordAttrib { get; } = new("vert_texcoord");
    public Attrib<Vector3> NormalAttrib { get; } = new("vert_normal");

    public Uniform<Matrix4> ProjectionMatrix { get; } = new("projection_matrix");
    public Uniform<Matrix4> ViewMatrix { get; } = new("view_matrix");
    public Uniform<Matrix4> ModelMatrix { get; } = new("model_matrix");

    public Uniform<GLTexture2D> Texture { get; } = new("tex");
    public Uniform<Color4> Tint { get; } = new("tint");
    public Uniform<RectangleF> SourceRect { get; } = new("source_rect");

    public TexturedShader()
        : base(ResourceLoader.LoadEmbedded<GLShaderProgram>("textured"))
    {
    }

    public override void Setup(Mesh<Vertex> mesh)
    {
        PositionAttrib.VertexAttribPointer<Vertex>(Vertex.PositionOffset);
        ColorAttrib.VertexAttribPointer<Vertex>(Vertex.ColorOffset);
        TexCoordAttrib.VertexAttribPointer<Vertex>(Vertex.TexCoordOffset);
        NormalAttrib.VertexAttribPointer<Vertex>(Vertex.NormalOffset);
    }

    public override void Setup(TexturedMaterial material, Camera camera, Transform? transform = null)
    {
        ProjectionMatrix.SetUniform(camera.ProjectionMatrix);
        ViewMatrix.SetUniform(camera.ViewMatrix);
        ModelMatrix.SetUniform(transform);

        Texture.SetUniform(material.Texture.GLTexture);
        SourceRect.SetUniform(material.Texture.SourceRectangle);
        Tint.SetUniform(material.Tint);
    }
}
