using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;

namespace Vildmark.Graphics.Fonts
{
    public class TextModel : IModel<TextMesh, TextMaterial>
    {
        public TextModel(TextMesh mesh, TextMaterial material)
        {
            Mesh = mesh;
            Material = material;
        }

        public Transform Transform { get; } = new();

        public TextMaterial Material { get; }
        
        public TextMesh Mesh { get; }
    }
}
