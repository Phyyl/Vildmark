using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
    public class TextShader : Shader, IPosition2Shader, ITexCoordShader, ITextureIndexShader, IModelViewProjectionMatrixShader, ITintShader, ITexturesShader
    {
        protected override string VertexShaderSource => ResourceLoader.LoadEmbedded<string>("text.vert");
        protected override string FragmentShaderSource => ResourceLoader.LoadEmbedded<string>("text.frag");

        public Attrib<Vector2> Position { get; } = new Attrib<Vector2>("vert_position");
        public Attrib<Vector2> TexCoord { get; } = new Attrib<Vector2>("vert_tex_coord");
        public Attrib<int> TextureIndex { get; } = new Attrib<int>("texture_index");
        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");
        public Uniform<Vector4> Tint { get; } = new Uniform<Vector4>("tint");
        public IndexedUniform<GLTexture2D> Textures { get; } = new IndexedUniform<GLTexture2D>("textures");

        public int MaxTextures => 8;
    }
}