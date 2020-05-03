using System;

namespace Vildmark.Graphics.Shaders
{
	public interface IShader
	{
		int GetAttribLocation(string name);
		int GetUniformLocation(string name);
		IDisposable Use();
	}
}