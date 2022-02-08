using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Windowing
{
    public partial class Window : IMouse
	{
        public Vector2 Delta => gameWindow.MouseState.Delta;
        public Vector2 Position => gameWindow.MouseState.Position;
        public Vector2 Wheel => gameWindow.MouseState.ScrollDelta;

        public bool CursorGrabbed
		{
			get => gameWindow.CursorGrabbed;
			set => gameWindow.CursorGrabbed = value;
		}

        public bool CursorVisible
        {
            get => gameWindow.CursorVisible;
            set => gameWindow.CursorVisible = value;
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
