using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;
using Vildmark.Graphics.Textures;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Msdf;

public class MsdfShader : Shader<Vertex, MsdfMaterial>
{
    public Attrib<Vector3> Position { get; } = new("vert_position");
    public Attrib<Vector2> TexCoord { get; } = new("vert_texcoord");

    public Uniform<Matrix4> ProjectionMatrix { get; } = new("projection_matrix");
    public Uniform<Matrix4> ViewMatrix { get; } = new("view_matrix");
    public Uniform<Matrix4> ModelMatrix { get; } = new("model_matrix");

    public Uniform<Texture2D> Texture { get; } = new("tex");
    public Uniform<Color4> BackgroundColor { get; } = new("background_color");
    public Uniform<Color4> ForegroundColor { get; } = new("foreground_color");
    public Uniform<float> PxRange { get; } = new("px_range");

    public MsdfShader()
        : base("msdf")
    {
    }

    public override void Setup(Mesh<Vertex> mesh)
    {
        Position.VertexAttribPointer<Vertex>(Vertex.PositionOffset);
        TexCoord.VertexAttribPointer<Vertex>(Vertex.TexCoordOffset);
    }

    public override void Setup(MsdfMaterial material, Camera camera, Transform? transform = null)
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
