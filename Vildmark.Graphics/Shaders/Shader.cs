using OpenTK.Graphics.OpenGL;
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
			return shaderProgram?.GetAttribLocation(name) ?? -1;
		}

		public int GetUniformLocation(string name)
		{
			return shaderProgram?.GetUniformLocation(name) ?? -1;
		}

		public IDisposable Use() => shaderProgram?.Use();
	}
}
