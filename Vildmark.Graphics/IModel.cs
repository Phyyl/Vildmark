using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics
{
    public interface IModel
    {
        Transform Transform { get; }

        void Render(IShader shader, ICamera camera);
    }

    public interface IModel<TMesh, TMaterial> : IModel
        where TMesh : IMesh
        where TMaterial : IMaterial
    {
        TMesh Mesh { get; }

        TMaterial Material { get; }
    }
}
