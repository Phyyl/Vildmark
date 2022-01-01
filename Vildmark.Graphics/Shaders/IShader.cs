using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;

namespace Vildmark.Graphics.Shaders
{
    public interface IShader<TMaterial>
    {
        VertexAttribPointerType GetAttribType(string name);
        int GetAttribSize(string name);

        void Begin(TMaterial material, Camera camera, Transform transform);

        void Use();

        void Uniform<T>(string name, T value);
        void Uniform<T>(int location, T value);

        int GetAttribLocation(string name);
        int GetUniformLocation(string name);
    }
}
