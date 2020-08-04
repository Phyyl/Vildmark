using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Graphics.Fonts.Resources
{
	public static class Shaders
	{
		public static DistanceFieldFontShader DistanceField { get; } = new DistanceFieldFontShader();
	}
}
