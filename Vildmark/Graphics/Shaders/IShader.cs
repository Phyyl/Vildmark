using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Meshes;

namespace Vildmark.Graphics.Shaders;

public interface IShader
{
    void Use();

    void Uniform<T>(string name, T value);
    void Uniform<T>(int location, T value);

    int GetAttribLocation(string name);
    int GetUniformLocation(string name);
}

public interface IShader<TVertex, TMaterial> : IShader
    where TVertex : unmanaged
{
    void Setup(Mesh<TVertex> mesh, TMaterial material, Camera camera, Transform? transform = default);
}
