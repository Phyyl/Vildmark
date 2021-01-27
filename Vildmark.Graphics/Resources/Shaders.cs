using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Resources
{
	public static class Shaders
	{
		public static MaterialShader Material { get; } = Service<EmbeddedShaderLoader>.Instance.Load<MaterialShader>("material");
	}
}
