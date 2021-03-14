using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Linq;

namespace Vildmark.Graphics.Shapes
{
    public abstract class ShapeMesh : Mesh<Vertex>
    {
        protected bool needsUpdate = true;

        protected abstract IEnumerable<Vertex> GenerateVertices();

        private void UpdateMesh()
        {
            base.VertexBuffer.SetData(GenerateVertices().ToArray());
        }

        protected override void OnRender(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            if (needsUpdate)
            {
                UpdateMesh();
                needsUpdate = false;
            }

            base.OnRender(primitiveType);
        }

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            needsUpdate = true;
        }
    }
}
