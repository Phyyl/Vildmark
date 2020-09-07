using OpenTK.Windowing.Common.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public partial class Window : IKeyboard
	{
		private KeyboardState previousKeyboardState;
		private KeyboardState keyboardState;

		public bool IsKeyDown(Key key) => keyboardState.IsKeyDown(key);

		public bool IsKeyUp(Key key) => keyboardState.IsKeyUp(key);

		public bool IsKeyPressed(Key key) => previousKeyboardState.IsKeyUp(key) && IsKeyDown(key);

		public bool IsKeyReleased(Key key) => previousKeyboardState.IsKeyDown(key) && IsKeyUp(key);

		private void UpdateKeyboard()
		{
			previousKeyboardState = keyboardState;
			keyboardState = gameWindow.KeyboardState;
		}
	}
}
