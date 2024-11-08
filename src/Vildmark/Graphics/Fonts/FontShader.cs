using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Shaders;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Fonts;

public class FontShader : Shader<Vertex, FontMaterial>
{
    public Attrib<Vertex, Vector3> Position { get; } = new("vert_position", Vertex.PositionOffset);
    public Attrib<Vertex, Vector2> TexCoord { get; } = new("vert_texcoord", Vertex.TexCoordOffset);

    public Uniform<Matrix4> ProjectionMatrix { get; } = new("projection_matrix");
    public Uniform<Matrix4> ViewMatrix { get; } = new("view_matrix");
    public Uniform<Matrix4> ModelMatrix { get; } = new("model_matrix");

    public Uniform<Texture2D> Texture { get; } = new("tex");
    public Uniform<Color4<Rgba>> BackgroundColor { get; } = new("background_color");
    public Uniform<Color4<Rgba>> ForegroundColor { get; } = new("foreground_color");
    public Uniform<float> PxRange { get; } = new("px_range");

    public FontShader()
        : base("msdf")
    {
    }

    public override void Setup(FontMaterial material, Camera camera, Transform? transform = null)
    {
        ProjectionMatrix.SetUniform(camera.ProjectionMatrix);
        ViewMatrix.SetUniform(camera.ViewMatrix);
        ModelMatrix.SetUniform(transform);

        Texture.SetUniform(material.Texture);
        BackgroundColor.SetUniform(material.BackgroundColor);
        ForegroundColor.SetUniform(material.ForegroundColor);
        PxRange.SetUniform(material.PxRange);
    }
}
