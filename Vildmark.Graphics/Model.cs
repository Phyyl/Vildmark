using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics
{
    public abstract class Model<TMesh, TMaterial, TShader> : IModel
        where TMesh : Mesh
        where TShader : Shader, IMaterialShader<TMaterial>, IMeshShader<TMesh>, IModelMatrixShader, ICameraShader
    {
        public Transform Transform { get; } = new();

        public TMesh Mesh { get; init; }
        public TMaterial Material { get; init; }
        public TShader Shader { get; init; }

        protected Model(TMesh mesh, TMaterial material, TShader shader)
        {
            Mesh = mesh;
            Material = material;
            Shader = shader;
        }

        public void Render(RenderContext renderContext)
        {
            renderContext.Render(Shader, Mesh, Material, Transform.Matrix);
        }
    }
}
