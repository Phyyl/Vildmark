using System;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
	public class Shader
	{
		private GLShaderProgram shaderProgram;

		public Shader(params GLShader[] shaders)
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
	}
}
