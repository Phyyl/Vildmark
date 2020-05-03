using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Graphics.Shaders
{
	public interface IUniformShader<T>
	{
		void SetupUniforms(T parameter);
	}
}
