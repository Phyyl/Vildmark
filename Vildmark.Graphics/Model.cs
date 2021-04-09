using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics
{
    public class Model<TMesh, TMaterial> : IModel<TMesh, TMaterial>
        where TMesh : IMesh
        where TMaterial : IMaterial
    {
        public Transform Transform { get; } = new();

        public TMesh Mesh { get; }

        public TMaterial Material { get; }

        protected Model(TMesh mesh, TMaterial material)
        {
            Mesh = mesh;
            Material = material;
        }

        public virtual void Render(IShader shader, ICamera camera)
        {
            shader.Use();

            if (shader is IModelMatrixShader modelMatrixShader)
            {
                modelMatrixShader.ModelMatrix.SetValue(Transform.Matrix);
            }

            camera.SetupShader(shader);
            Mesh.SetupShader(shader);
            Material.SetupShader(shader);

            Mesh.Render();
        }
    }

    public class Model : Model<Mesh, ModelMaterial>
    {
        public Model(Mesh mesh, ModelMaterial material = default)
            : base(mesh, material ?? new(Texture2D.WhitePixel))
        {
        }
    }
}
