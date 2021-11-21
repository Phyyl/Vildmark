using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Shapes
{
    public abstract class ShapeModel : Model
    {
        protected bool needsUpdate = true;

        protected ShapeModel(ModelMaterial material = null)
            : base(new Mesh(), material)
        {
        }

        protected abstract IEnumerable<Vertex> GenerateVertices();

        private void UpdateMesh()
        {
            Mesh.VertexBuffer.SetData(GenerateVertices().ToArray());
        }

        public override void Render(IShader shader, ICamera camera)
        {
            if (needsUpdate)
            {
                UpdateMesh();
                needsUpdate = false;
            }

            base.Render(shader, camera);
        }

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            needsUpdate = true;
        }
    }
}
