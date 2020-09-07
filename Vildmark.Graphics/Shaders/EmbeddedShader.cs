using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
	public abstract class EmbeddedShader : IShader
	{
		private readonly string name;
		private readonly Shader shader;

		protected virtual Assembly EmbeddedResourceAssembly => GetType().Assembly;
		protected virtual string BaseNamespace => GetType().Assembly.GetName().Name;
		protected virtual string ResourceNamespace => $"{BaseNamespace}.Resources.Shaders";

		protected EmbeddedShader(string name)
		{
			this.name = name;

			shader = new Shader(LoadVertexShader(), LoadFragmentShader(), LoadGeometryShader());
		}

		protected virtual GLShader LoadFragmentShader()
		{
			return LoadShader(ShaderType.FragmentShader);
		}

		protected virtual GLShader LoadVertexShader()
		{
			return LoadShader(ShaderType.VertexShader);
		}

		protected virtual GLShader LoadGeometryShader()
		{
			return LoadShader(ShaderType.GeometryShader);
		}

		protected virtual GLShader LoadShader(ShaderType shaderType)
		{
			return GLShader.Create(shaderType, EmbeddedResources.Get<string>(GetResourceName(shaderType), EmbeddedResourceAssembly));
		}

		protected virtual string GetResourceName(ShaderType shaderType)
		{
			string suffix = shaderType switch
			{
				ShaderType.FragmentShader => "frag",
				ShaderType.GeometryShader => "geom",
				_ => "vert"
			};

			return $"{ResourceNamespace}.{name}.{suffix}";
		}

		public int GetAttribLocation(string name)
		{
			return shader.GetAttribLocation(name);
		}

		public int GetUniformLocation(string name)
		{
			return shader.GetUniformLocation(name);
		}

		public IDisposable Use()
		{
			return shader.Use();
		}
	}
}
