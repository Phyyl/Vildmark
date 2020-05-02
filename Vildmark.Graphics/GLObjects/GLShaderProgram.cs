using OpenToolkit.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Linq;

namespace Vildmark.Graphics.GLObjects
{
	public class GLShaderProgram : GLObject
	{
		private GLShaderProgram(params GLShader[] shaders)
			: base(GL.CreateProgram())
		{
			foreach (var shader in shaders)
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

		public IDisposable Use()
		{
			return new UseContext(this);
		}

		public void Unuse()
		{
			GL.UseProgram(0);
		}

		public int GetUniformLocation(string name)
		{
			return GL.GetUniformLocation(this, name);
		}

		public int GetAttribLocation(string name)
		{
			return GL.GetAttribLocation(this, name);
		}

		protected override void DisposeOpenGL()
		{
			GL.DeleteProgram(this);
		}

		public static GLShaderProgram Create(params GLShader[] shaders)
		{
			shaders = shaders.Where(s => s is { }).ToArray();

			if (shaders.Any(s => !s.Compiled))
			{
				return null;
			}

			GLShaderProgram shaderProgram = new GLShaderProgram(shaders);

			if (!shaderProgram.Linked)
			{
				Debug.WriteLine($"Shader program info log: {shaderProgram.InfoLog}");

				return null;
			}

			return shaderProgram;
		}

		private class UseContext : IDisposable
		{
			public GLShaderProgram ShaderProgram { get; }

			public UseContext(GLShaderProgram shaderProgram, bool use = true)
			{
				ShaderProgram = shaderProgram;

				if (use)
				{
					GL.UseProgram(ShaderProgram);
				}
			}

			public void Dispose()
			{
				ShaderProgram.Unuse();
			}
		}
	}
}
