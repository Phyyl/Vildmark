using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Fonts
{
    public class TextMesh : Mesh<TextVertex>
    {
        public TextMesh(Span<TextVertex> vertices = default)
            : base(vertices)
        {
        }

        public override void SetupShader(IShader shader)
        {
            base.SetupShader(shader);

            if (shader is ITextureIndexShader textureIndexShader)
            {
                textureIndexShader.TextureIndex.Setup(VertexBuffer, TextVertex.TextureIndexOffset);
            }

            if (shader is IPosition2Shader positionShader)
            {
                positionShader.Position.Setup(VertexBuffer, TextVertex.PositionOffset);
            }

            if (shader is ITexCoordShader texCoordShader)
            {
                texCoordShader.TexCoord.Setup(VertexBuffer, TextVertex.TexCoordOffset);
            }
        }
    }
}
