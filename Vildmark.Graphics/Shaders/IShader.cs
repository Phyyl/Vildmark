using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;

namespace Vildmark.Graphics.Shaders
{
    //public interface IShader<TMesh, TMaterial, TShaderSetupInfo>
    //    where TMesh : Mesh
    //    where TMaterial : Material
    //{
    //    void Setup(TMesh mesh, TMaterial material, Camera camera, TShaderSetupInfo setupInfo);
    //}

    //public interface IShader<TMesh, TMaterial> : IShader<TMesh, TMaterial, Matrix4>
    //    where TMesh : Mesh
    //    where TMaterial : Material
    //{
    //}

    public interface ISetupShader<TSetupInfo>
    {
        void Setup(TSetupInfo setupInfo);
    }

    public interface IModelMatrixShader
    {
        void SetupModelMatrix(Matrix4 modelMatrix);
    }

    public interface IMeshShader<TMesh>
        where TMesh : Mesh
    {
        void SetupMesh(TMesh mesh);
    }

    public interface ICameraShader
    {
        void SetupCamera(Camera camera);
    }

    public interface IMaterialShader<TMaterial>
    {
        void SetupMaterial(TMaterial material);
    }
}
