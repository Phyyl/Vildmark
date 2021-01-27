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
        public Transform Transform { get; } = new Transform();

        public virtual Mesh Mesh { get; }

        public Material Material { get; }

        public Model(Mesh mesh, Material material, Vector3 position = default, Vector3 rotation = default)
        {
            Mesh = mesh;
            Material = material;
            Transform.Position = position;
            Transform.Rotation = rotation;
        }
    }
}
