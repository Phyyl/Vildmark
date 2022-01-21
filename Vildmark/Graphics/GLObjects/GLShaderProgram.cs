using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Vildmark.Graphics.Rendering;

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

        public bool Uniform<T>(string name, T value) => Uniform(GetUniformLocation(name), value);
        public bool Uniform<T>(int location, T value) => StructTypeInfo.SetUniform(location, value);

        protected override void DisposeOpenGL()
        {
            GL.DeleteProgram(this);
        }

        public static void Unuse()
        {
            GL.UseProgram(0);
        }

        public static GLShaderProgram? Create(params GLShader?[] shaders)
        {
            if (shaders.Any(s => s is not null && !s.Compiled))
            {
                return null;
            }

            GLShaderProgram shaderProgram = new(shaders.NotNull().ToArray());

            if (!shaderProgram.Linked)
            {
                Console.WriteLine($"Shader program info log: {shaderProgram.InfoLog}");

                return null;
            }

            return shaderProgram;
        }
    }
}
