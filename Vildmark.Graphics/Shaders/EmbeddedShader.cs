using OpenToolkit.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
	public abstract class EmbeddedShader : Shader
	{
		private readonly string name;

		protected virtual Assembly EmbeddedResourceAssembly => GetType().Assembly;
		protected virtual string BaseNamespace => GetType().Assembly.GetName().Name;
		protected virtual string ResourceNamespace => $"{BaseNamespace}.Resources.Shaders";

		protected EmbeddedShader(string name)
		{
			this.name = name;
		}

		protected override GLShader LoadFragmentShader()
		{
			return LoadShader(ShaderType.FragmentShader);
		}

		protected override GLShader LoadVertexShader()
		{
			return LoadShader(ShaderType.VertexShader);
		}

		protected override GLShader LoadGeometryShader()
		{
			return LoadShader(ShaderType.GeometryShader);
		}

		protected virtual GLShader LoadShader(ShaderType shaderType)
		{
			return GLShader.Create(shaderType, EmbeddedResources.GetString(GetResourceName(shaderType), EmbeddedResourceAssembly));
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
	}
}
