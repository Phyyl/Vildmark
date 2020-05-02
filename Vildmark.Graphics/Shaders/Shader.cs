using OpenToolkit.Graphics.OpenGL;
using System;
using System.Reflection;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
	public abstract class Shader
	{
		private GLShaderProgram shaderProgram;

		protected Shader()
		{
			shaderProgram = GLShaderProgram.Create(LoadVertexShader(), LoadFragmentShader(), LoadGeometryShader());
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

		protected abstract GLShader LoadVertexShader();

		protected abstract GLShader LoadFragmentShader();

		protected abstract GLShader LoadGeometryShader();
	}
}
