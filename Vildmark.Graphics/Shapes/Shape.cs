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
    public abstract class Shape
    {
        protected bool needsUpdate = true;

        private Mesh mesh;

        public Mesh Mesh
        {
            get
            {
                if (needsUpdate)
                {
                    UpdateMesh();
                    needsUpdate = false;
                }

                return mesh;
            }
        }

        protected Shape()
        {
            mesh = new Mesh();
        }

        protected abstract IEnumerable<Vertex> GenerateVertices();

        private void UpdateMesh()
        {
            mesh.VertexBuffer.SetData(GenerateVertices().ToArray());
        }

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            needsUpdate = true;
        }
    }
}
