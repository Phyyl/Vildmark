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
        where TShader : IMaterialShader<TMaterial>, IMeshShader<TMesh>
    {
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
            if (Shader is ICameraShader cameraShader)
            {
                cameraShader.SetupCamera(renderContext.Camera);
            }

            Shader.SetupMaterial(Material);
            Shader.SetupMesh(Mesh);

            Mesh.Render();
        }
    }
}
