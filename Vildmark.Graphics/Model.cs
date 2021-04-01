using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics
{
    public class Model : IModel<Mesh, ModelMaterial>
    {
        public Transform Transform { get; } = new();

        public Mesh Mesh { get; }

        public ModelMaterial Material { get; }

        public Model(Mesh mesh, ModelMaterial material = default)
        {
            Mesh = mesh;
            Material = material ?? new(Texture2D.WhitePixel);
        }

        public virtual void SetupShader(IShader shader)
        {
            if (shader is IModelMatrixShader modelMatrixShader)
            {
                modelMatrixShader.ModelMatrix.SetValue(Transform.Matrix);
            }
        }
    }
}
