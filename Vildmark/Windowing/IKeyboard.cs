using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Windowing
{
    public interface IKeyboard
	{
        event Action<Keys>? OnKeyPressed;
        event Action<Keys>? OnKeyReleased;

        bool IsKeyDown(Keys key);
		bool IsKeyUp(Keys key);
		bool IsKeyPressed(Keys key);
		bool IsKeyReleased(Keys key);
	}
}
