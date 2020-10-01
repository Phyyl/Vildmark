using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public interface IKeyboard
	{
		bool IsKeyDown(Keys key);
		bool IsKeyUp(Keys key);
		bool IsKeyPressed(Keys key);
		bool IsKeyReleased(Keys key);
	}
}
