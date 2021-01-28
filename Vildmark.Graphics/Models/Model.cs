using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.Models
{
    public class Model
    {
        public virtual Mesh Mesh { get; }

        public Material Material { get; }

        public Model(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }
    }
}
