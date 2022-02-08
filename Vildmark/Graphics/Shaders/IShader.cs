using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.Shaders
{
    public interface IShader
    {
        VertexAttribPointerType GetAttribType(string name);
        int GetAttribSize(string name);

        void Use();

        void Uniform<T>(string name, T value);
        void Uniform<T>(int location, T value);

        int GetAttribLocation(string name);
        int GetUniformLocation(string name);
    }

    public interface IShaderSetup<TInput>
    {
        void Setup(TInput input);
    }

    public interface IShaderMaterialSetup
    {
        void Setup<TMaterial>(TMaterial material);
    }
}
