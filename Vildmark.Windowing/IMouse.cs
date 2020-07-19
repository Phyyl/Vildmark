using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public interface IMouse
	{
		Vector2 Delta { get; }
		Vector2 Position { get; }

		bool IsMouseGrabbed { get; set; }

		bool IsMouseDown(MouseButton mouseButton);
		bool IsMouseUp(MouseButton mouseButton);
		bool IsMousePressed(MouseButton mouseButton);
		bool IsMouseReleased(MouseButton mouseButton);
	}
}
