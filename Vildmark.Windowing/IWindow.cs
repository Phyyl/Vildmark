using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public interface IWindow : IMouse, IKeyboard
	{
		int Width { get; }

		int Height { get; }

		Vector2i Size { get; }

		IWindowHandler WindowHandler { get; set; }

		void Run();
	}
}
