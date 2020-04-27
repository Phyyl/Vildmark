using System;
using System.Collections.Generic;
using System.Text;

namespace Ashborn.Graphics.Shaders
{
	public abstract class ShaderVariable
	{
		protected ShaderVariable(Shader shader, string name)
		{
			Name = name;
			Shader = shader;
			Location = GetLocation();
		}

		public string Name { get; }

		public Shader Shader { get; }

		public int Location { get; }

		public bool Defined => Location >= 0;

		protected abstract int GetLocation();
	}
}
