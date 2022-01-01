using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Models;

namespace Vildmark.Graphics.Fonts
{
    public class TextModel : Model<TextVertex, TextMaterial, TextShader>
    {
        public TextModel(TextMesh mesh, TextMaterial material)
            : base(mesh, material, new())
        {
        }

        protected override void SetupAttribs()
        {
            AttribPointer("vert_position", nameof(TextVertex.Position));
            AttribPointer("vert_texcoord", nameof(TextVertex.TexCoord));
            AttribPointer("vert_texture_index", nameof(TextVertex.TextureIndex));
        }
    }
}
