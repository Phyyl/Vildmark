using OpenToolkit.Graphics.OpenGL;
using System;
using System.Reflection;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
	public class Shader : IShader
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
