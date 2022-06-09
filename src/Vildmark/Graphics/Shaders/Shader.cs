using OpenTK.Graphics.OpenGL4;
using System.Collections.Immutable;
using System.Reflection;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders;

public abstract class Shader<TVertex, TMaterial> : IShader<TVertex, TMaterial>
    where TVertex : unmanaged
{
    private readonly GLShaderProgram shaderProgram;
    private readonly ImmutableArray<Attrib> attribs;
    private readonly ImmutableArray<Uniform> uniforms;

    protected Shader(string resourceName)
        : this(EmbeddedResources.GetEmbeddedStream($"{resourceName}.vert", Assembly.GetCallingAssembly()).ReadAllText(),
              EmbeddedResources.GetEmbeddedStream($"{resourceName}.frag", Assembly.GetCallingAssembly()).ReadAllText())
    {
    }

    protected Shader(string vertexShaderSource, string fragmentShaderSource)
    {
        shaderProgram = new GLShaderProgram();

        GLShader vertexShader = new(ShaderType.VertexShader, vertexShaderSource);
        GLShader fragmentShader = new(ShaderType.FragmentShader, fragmentShaderSource);

        if (!vertexShader.Compile())
        {
            throw new Exception("Vertex shader compilation failed");
        }

        if (!fragmentShader.Compile())
        {
            throw new Exception("Fragment shader compilation failed");
        }

        shaderProgram.AttachShader(vertexShader);
        shaderProgram.AttachShader(fragmentShader);

        if (!shaderProgram.Link())
        {
            throw new Exception("Shader program link failed");
        }
        
        uniforms = this.GetInstancePropertiesOfType<Uniform>().ToImmutableArray();
        attribs = this.GetInstancePropertiesOfType<Attrib>().ToImmutableArray();

        InitializeVariableLocations();
    }

    public void Use()
    {
        shaderProgram?.Use();
    }

    public void Uniform<T>(string name, T value) => shaderProgram.Uniform(name, value);
    public void Uniform<T>(int location, T value) => shaderProgram.Uniform(location, value);

    public int GetAttribLocation(string name) => shaderProgram?.GetAttribLocation(name) ?? -1;
    public int GetUniformLocation(string name) => shaderProgram?.GetUniformLocation(name) ?? -1;

    private void InitializeVariableLocations()
    {
        foreach (var uniform in uniforms)
        {
            uniform.Location = GetUniformLocation(uniform.Name);
        }

        foreach (var attrib in attribs)
        {
            attrib.Location = GetAttribLocation(attrib.Name);
        }
    }

    public abstract void Setup(TMaterial material, Camera camera, Transform? transform = null);

    public void Setup(Mesh<TVertex> mesh)
    {
        foreach (var attrib in attribs)
        {
            attrib.VertexAttribPointer();
        }
    }
}
