using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Shapes
{
    public abstract class Shape : Model
    {
        protected bool needsUpdate = true;

        public override Mesh Mesh
        {
            get
            {
                if (needsUpdate)
                {
                    UpdateMesh();
                    needsUpdate = false;
                }

                return base.Mesh;
            }
        }

        protected Shape(Material material, Vector3 position = default, Vector3 rotation = default)
            : base(new Mesh(), material, position, rotation)
        {
        }

        protected abstract IEnumerable<Vertex> GenerateVertices();

        private void UpdateMesh()
        {
            base.Mesh.VertexBuffer.SetData(GenerateVertices().ToArray());
        }

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            needsUpdate = true;
        }
    }
}
