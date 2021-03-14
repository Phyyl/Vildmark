using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Linq;

namespace Vildmark.Graphics.GLObjects
{
    public class GLShaderProgram : GLObject
    {
        private GLShaderProgram(params GLShader[] shaders)
            : base(GL.CreateProgram())
        {
            foreach (var shader in shaders.Where(s => s is not null))
            {
                GL.AttachShader(this, shader);
            }

            GL.LinkProgram(this);
        }

        public string InfoLog => GL.GetProgramInfoLog(this);

        public bool Linked
        {
            get
            {
                GL.GetProgram(this, GetProgramParameterName.LinkStatus, out int value);

                return value == 1;
            }
        }

        public void Use()
        {
            GL.UseProgram(this);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(this, name);
        }

        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(this, name);
        }

        public void Uniform(string name, Matrix4 value) => GL.UniformMatrix4(GetUniformLocation(name), false, ref value);
        public void Uniform(string name, int value) => GL.Uniform1(GetUniformLocation(name), value);
        public void Uniform(string name, float value) => GL.Uniform1(GetUniformLocation(name), value);
        public void Uniform(string name, Vector2 value) => GL.Uniform2(GetUniformLocation(name), value);
        public void Uniform(string name, Vector3 value) => GL.Uniform3(GetUniformLocation(name), value);
        public void Uniform(string name, Vector4 value) => GL.Uniform4(GetUniformLocation(name), value);

        protected override void DisposeOpenGL()
        {
            GL.DeleteProgram(this);
        }

        public static void Unuse()
        {
            GL.UseProgram(0);
        }

        public static GLShaderProgram Create(params GLShader[] shaders)
        {
            shaders = shaders.Where(s => s is { }).ToArray();

            if (shaders.Any(s => !s.Compiled))
            {
                return null;
            }

            GLShaderProgram shaderProgram = new(shaders);

            if (!shaderProgram.Linked)
            {
                Debug.WriteLine($"Shader program info log: {shaderProgram.InfoLog}");

                return null;
            }

            return shaderProgram;
        }
    }
}
