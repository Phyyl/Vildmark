using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
    public class Shader : IShader
    {
        private record class Attrib(int Size, VertexAttribPointerType Type);

        private readonly Dictionary<string, Attrib> attribs = new();
        private readonly GLShaderProgram shaderProgram;

        protected Shader(string vertexShaderSource, string fragmentShaderSource, string? geometryShaderSource = default)
        {
            shaderProgram = GLShaderProgram.Create(
                GLShader.CreateVertex(vertexShaderSource),
                GLShader.CreateFragment(fragmentShaderSource),
                GLShader.CreateGeometry(geometryShaderSource)) ??
                throw new Exception("Could not create shader program");

            InitializeAttribs();
        }

        public void Use() => shaderProgram?.Use();

        public void Uniform<T>(string name, T value) => shaderProgram.Uniform(name, value);
        public void Uniform<T>(int location, T value) => shaderProgram.Uniform(location, value);

        public int GetAttribLocation(string name) => shaderProgram?.GetAttribLocation(name) ?? -1;
        public int GetUniformLocation(string name) => shaderProgram?.GetUniformLocation(name) ?? -1;

        public VertexAttribPointerType GetAttribType(string name) => attribs.GetValueOrDefault(name)?.Type ?? VertexAttribPointerType.Float;
        public int GetAttribSize(string name) => attribs.GetValueOrDefault(name)?.Size ?? 1;

        private void InitializeAttribs()
        {
            GL.GetProgram(shaderProgram.ID, GetProgramParameterName.ActiveAttributes, out int count);

            for (int i = 0; i < count; i++)
            {
                GL.GetActiveAttrib(shaderProgram.ID, i, 256, out _, out _, out ActiveAttribType activeAttribType, out string name);

                attribs[name] = activeAttribType switch
                {
                    ActiveAttribType.FloatVec2 => new(2, VertexAttribPointerType.Float),
                    ActiveAttribType.FloatVec3 => new(3, VertexAttribPointerType.Float),
                    ActiveAttribType.FloatVec4 => new(4, VertexAttribPointerType.Float),
                    ActiveAttribType.Int => new(1, VertexAttribPointerType.Int),
                    ActiveAttribType.UnsignedInt => new(1, VertexAttribPointerType.UnsignedInt),
                    _ => new(1, VertexAttribPointerType.Float),
                };
            }
        }
    }
}
