﻿using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public partial class Window : IKeyboard
	{
		public bool IsKeyDown(Keys key) => gameWindow.IsKeyDown(key);

		public bool IsKeyUp(Keys key) => !IsKeyDown(key);

		public bool IsKeyPressed(Keys key) => gameWindow.IsKeyPressed(key);

		public bool IsKeyReleased(Keys key) => gameWindow.IsKeyReleased(key);
	}
}
