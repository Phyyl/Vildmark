using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;

namespace Vildmark.Graphics.Shaders;

public abstract class Shader<TVertex, TMaterial> : IShader<TVertex, TMaterial>
    where TVertex : unmanaged
{
    private readonly GLShaderProgram shaderProgram;

    protected Shader(GLShaderProgram shaderProgram)
    {
        this.shaderProgram = shaderProgram;

        InitializeVariables();
    }

    public void Use()
    {
        shaderProgram?.Use();
    }

    public void Uniform<T>(string name, T value) => shaderProgram.Uniform(name, value);
    public void Uniform<T>(int location, T value) => shaderProgram.Uniform(location, value);

    public int GetAttribLocation(string name) => shaderProgram?.GetAttribLocation(name) ?? -1;
    public int GetUniformLocation(string name) => shaderProgram?.GetUniformLocation(name) ?? -1;

    private void InitializeVariables()
    {
        var uniforms = this.GetInstancePropertiesOfType<Uniform>();

        foreach (var uniform in uniforms)
        {
            uniform.Location = GetUniformLocation(uniform.Name);
        }

        var attribs = this.GetInstancePropertiesOfType<Attrib>();

        foreach (var attrib in attribs)
        {
            attrib.Location = GetAttribLocation(attrib.Name);
        }
    }

    public abstract void Setup(TMaterial material, Camera camera, Transform? transform = null);
    public abstract void Setup(Mesh<TVertex> mesh);
}
