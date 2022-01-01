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
    }
}
