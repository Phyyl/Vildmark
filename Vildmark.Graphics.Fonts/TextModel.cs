using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Fonts
{
    public class TextModel : Model<TextMesh, TextMaterial>
    {
        public TextModel(TextMesh mesh, TextMaterial material)
            : base(mesh, material)
        {
        }
    }
}
