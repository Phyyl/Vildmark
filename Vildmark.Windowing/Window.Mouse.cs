using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Windowing
{
    public partial class Window : IMouse
	{
		public Vector2 Delta { get; private set; }
		public Vector2 Position { get; private set; }

		public bool IsMouseGrabbed
		{
			get => gameWindow.CursorGrabbed;
			set
			{
				gameWindow.CursorVisible = !value;
				gameWindow.CursorGrabbed = value;
			}
		}

		public bool IsMouseDown(MouseButton mouseButton)
		{
			return gameWindow.IsMouseButtonDown(mouseButton);
		}

		public bool IsMousePressed(MouseButton mouseButton)
		{
			return gameWindow.IsMouseButtonPressed(mouseButton);
		}

		public bool IsMouseReleased(MouseButton mouseButton)
		{
			return gameWindow.IsMouseButtonReleased(mouseButton);
		}

		public bool IsMouseUp(MouseButton mouseButton)
		{
			return !gameWindow.IsMouseButtonDown(mouseButton);
		}
	}
}
