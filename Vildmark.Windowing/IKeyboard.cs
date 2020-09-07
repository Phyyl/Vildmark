using OpenTK.Windowing.Common.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public interface IKeyboard
	{
		bool IsKeyDown(Key key);
		bool IsKeyUp(Key key);
		bool IsKeyPressed(Key key);
		bool IsKeyReleased(Key key);
	}
}
