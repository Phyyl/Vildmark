using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.Fonts
{
    public class TextModel : Model<Mesh<TextVertex>, TextMaterial, TextShader>
    {
        public TextModel(Mesh<TextVertex> mesh, TextMaterial material, TextShader shader)
            : base(mesh, material, shader)
        {
        }
    }
}
