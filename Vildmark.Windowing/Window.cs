using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;
using OpenToolkit.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
	public partial class Window : IWindow
	{
		private GameWindow gameWindow;
		private readonly WindowSettings settings;

		public IWindowHandler WindowHandler { get; set; }

		public int Width => Size.X;

		public int Height => Size.Y;

		public Vector2i Size { get; private set; }

		public Window(WindowSettings settings, IWindowHandler windowHandler)
		{
			GLFW.WindowHint(WindowHintInt.Samples, settings.Samples);

			gameWindow = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
			{
				Size = new Vector2i(settings.Width, settings.Height),
				Title = settings.Title,
				WindowState = settings.State,
				WindowBorder = settings.Border,
				//Profile = ContextProfile.Core,
				//API = ContextAPI.OpenGL,
				//APIVersion = new Version(4, 6),
			});

			gameWindow.Load += GameWindow_Load;
			gameWindow.Unload += GameWindow_Unload;
			gameWindow.Resize += GameWindow_Resize;
			gameWindow.UpdateFrame += GameWindow_UpdateFrame;
			gameWindow.RenderFrame += GameWindow_RenderFrame;
			WindowHandler = windowHandler;

			this.settings = settings;
		}

		public void Run()
		{
			gameWindow.Run();
		}

		private void GameWindow_Load()
		{
			WindowHandler?.Load();

			if (settings.Samples > 0)
			{
				GL.Enable(EnableCap.Multisample);
			}
		}

		private void GameWindow_Unload()
		{
			WindowHandler?.Unload();
		}

		private void GameWindow_Resize(ResizeEventArgs obj)
		{
			Size = new Vector2i(obj.Width, obj.Height);

			WindowHandler?.Resize(obj.Width, obj.Height);
		}

		private void GameWindow_UpdateFrame(FrameEventArgs obj)
		{
			UpdateKeyboard();

			WindowHandler?.Update((float)obj.Time);
		}

		private void GameWindow_RenderFrame(FrameEventArgs obj)
		{
			GL.Viewport(0, 0, Width, Height);

			WindowHandler?.Render((float)obj.Time);

			gameWindow.SwapBuffers();
		}
	}
}
