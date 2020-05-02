using OpenToolkit.Graphics.OpenGL;
using System;
using System.Reflection;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
	public class Shader
	{
		private GLShaderProgram shaderProgram;

		protected Shader(params GLShader[] shaders)
		{
			shaderProgram = GLShaderProgram.Create(shaders);
		}

		public int GetAttribLocation(string name)
		{
			return shaderProgram.GetAttribLocation(name);
		}

		public int GetUniformLocation(string name)
		{
			return shaderProgram.GetUniformLocation(name);
		}

		public IDisposable Use() => shaderProgram.Use();

		protected virtual GLShader LoadVertexShader(string name)
		{
			return LoadShader(ShaderType.VertexShader, name);
		}

		protected virtual GLShader LoadFragmentShader(string name)
		{
			return LoadShader(ShaderType.FragmentShader, name);
		}

		protected virtual GLShader LoadShader(ShaderType shaderType, string name)
		{
			return GLShader.Create(shaderType, EmbeddedResources.GetString(name, Assembly.GetCallingAssembly()));
		}
	}
}
