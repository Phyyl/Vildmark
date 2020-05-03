using OpenToolkit.Graphics.OpenGL;
using System.Diagnostics;

namespace Vildmark.Graphics.GLObjects
{
    public class GLShader : GLObject
    {
        private GLShader(ShaderType shaderType, string source)
            : base(GL.CreateShader(shaderType))
        {
            GL.ShaderSource(this, source);
            GL.CompileShader(this);
        }

        public string InfoLog => GL.GetShaderInfoLog(this);

        public bool Compiled
        {
            get
            {
                GL.GetShader(this, ShaderParameter.CompileStatus, out int value);

                return value == 1;
            }
        }

        protected override void DisposeOpenGL()
        {
            GL.DeleteShader(this);
        }

        public static GLShader Create(ShaderType shaderType, string source)
        {
            if (source == default)
            {
                return default;
            }

            GLShader shader = new GLShader(shaderType, source);

            if (!shader.Compiled)
            {
                Debug.WriteLine($"{shaderType} info log: {shader.InfoLog}");

                return null;
            }

            return shader;
        }
    }
}
