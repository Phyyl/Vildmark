using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Resources
{
	public static class Shaders
	{
		public static MaterialShader DistanceField { get; } = Service<EmbeddedShaderLoader>.Instance.Load<MaterialShader>("font");
	}
}
