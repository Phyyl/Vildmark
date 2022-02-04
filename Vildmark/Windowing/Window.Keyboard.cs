using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Windowing
{
    public partial class Window : IKeyboard
    {
        public event Action<Keys>? OnKeyPressed;

        public event Action<Keys>? OnKeyReleased;

        public bool IsKeyDown(Keys key) => gameWindow.IsKeyDown(key);

		public bool IsKeyUp(Keys key) => !IsKeyDown(key);

		public bool IsKeyPressed(Keys key) => gameWindow.KeyboardState.IsKeyDown(key) && !gameWindow.KeyboardState.WasKeyDown(key);

		public bool IsKeyReleased(Keys key) => !gameWindow.KeyboardState.IsKeyDown(key) && gameWindow.KeyboardState.WasKeyDown(key);

        private void GameWindow_KeyDown(KeyboardKeyEventArgs obj)
        {
            OnKeyPressed?.Invoke(obj.Key);
        }

        private void GameWindow_KeyUp(KeyboardKeyEventArgs obj)
        {
            OnKeyReleased?.Invoke(obj.Key);
        }
    }
}
