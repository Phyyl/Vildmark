using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts;

public class BitmapFontShader : Shader<BitmapFontVertex, BitmapFontMaterial>
{
    public Attrib<Vector2> PositionAttrib { get; } = new("vert_position");
    public Attrib<Vector2> TexCoordAttrib { get; } = new("vert_tex_coord");
    public Attrib<int> PageIndexAttrib { get; } = new("vert_page_index");

    public Uniform<Matrix4> ProjectionMatrix { get; } = new("projection_matrix");
    public Uniform<Matrix4> ViewMatrix { get; } = new("view_matrix");
    public Uniform<Matrix4> ModelMatrix { get; } = new("model_matrix");
    public Uniform<GLTexture2D[]> Textures { get; } = new("textures");
    public Uniform<Color4> Tint { get; } = new("tint");

    public BitmapFontShader()
        : base(ResourceLoader.LoadEmbedded<GLShaderProgram>("bitmapfont"))
    {
    }

    public override void Setup(Mesh<BitmapFontVertex> mesh, BitmapFontMaterial material, Camera camera, Transform? transform = null)
    {
        mesh.VertexArray.Bind();

        PositionAttrib.VertexAttribPointer<BitmapFontVertex>(BitmapFontVertex.PositionOffset);
        TexCoordAttrib.VertexAttribPointer<BitmapFontVertex>(BitmapFontVertex.TexCoordOffset);
        PageIndexAttrib.VertexAttribPointer<BitmapFontVertex>(BitmapFontVertex.PageIndexOffset);

        ProjectionMatrix.SetUniform(camera.ProjectionMatrix);
        ViewMatrix.SetUniform(camera.ViewMatrix);
        ModelMatrix.SetUniform(transform);
        Textures.SetUniform(material.Textures);
        Tint.SetUniform(material.Color);
    }
}
