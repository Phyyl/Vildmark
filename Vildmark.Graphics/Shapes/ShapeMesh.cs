using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Linq;
using Vildmark.Graphics.Meshes;

namespace Vildmark.Graphics.Shapes
{
    public abstract class ShapeMesh : Mesh
    {
        protected bool needsUpdate = true;

        protected abstract IEnumerable<Vertex> GenerateVertices();

        private void UpdateMesh()
        {
            base.VertexBuffer.SetData(GenerateVertices().ToArray());
        }

        public override void Render(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            if (needsUpdate)
            {
                UpdateMesh();
                needsUpdate = false;
            }

            Render(primitiveType);
        }

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            needsUpdate = true;
        }
    }
}
