using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Shaders;

public class TexturedShader : Shader<Vertex, TexturedMaterial>
{
    public Attrib<Vertex, Vector3> PositionAttrib { get; } = new("vert_position", Vertex.PositionOffset);
    public Attrib<Vertex, Vector4> ColorAttrib { get; } = new("vert_color", Vertex.ColorOffset);
    public Attrib<Vertex, Vector2> TexCoordAttrib { get; } = new("vert_texcoord", Vertex.TexCoordOffset);
    public Attrib<Vertex, Vector3> NormalAttrib { get; } = new("vert_normal", Vertex.NormalOffset);

    public Uniform<Matrix4> ProjectionMatrix { get; } = new("projection_matrix");
    public Uniform<Matrix4> ViewMatrix { get; } = new("view_matrix");
    public Uniform<Matrix4> ModelMatrix { get; } = new("model_matrix");

    public Uniform<Texture2D> Texture { get; } = new("tex");
    public Uniform<Color4> Tint { get; } = new("tint");
    public Uniform<Box2> SourceRect { get; } = new("source_rect");

    public TexturedShader()
        : base("textured")
    {
    }

    public override void Setup(TexturedMaterial material, Camera camera, Transform? transform = null)
    {
        ProjectionMatrix.SetUniform(camera.ProjectionMatrix);
        ViewMatrix.SetUniform(camera.ViewMatrix);
        ModelMatrix.SetUniform(transform);

        Texture.SetUniform(material.Texture);
        Tint.SetUniform(material.Tint);

        Box2 sourceRect = (material.Texture as SubTexture2D)?.SourceRectangle ?? new(0, 0, 1, 1);
        SourceRect.SetUniform(sourceRect);
    }
}
