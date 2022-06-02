using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Windowing.Input;

public class Keyboard : InputDevice
{
    public event Action<Keys>? OnKeyPressed;
    public event Action<Keys>? OnKeyReleased;
    public event Action<string>? OnTextInput;

    internal Keyboard()
    {
        GameWindow.KeyDown += e => OnKeyPressed?.Invoke(e.Key);
        GameWindow.KeyUp += e => OnKeyReleased?.Invoke(e.Key);
        GameWindow.TextInput += e => OnTextInput?.Invoke(e.AsString);
    }

    public bool IsKeyDown(Keys key) => GameWindow.IsKeyDown(key);
    public bool IsKeyUp(Keys key) => !IsKeyDown(key);
    public bool IsKeyPressed(Keys key) => GameWindow.KeyboardState.IsKeyDown(key) && !GameWindow.KeyboardState.WasKeyDown(key);
    public bool IsKeyReleased(Keys key) => !GameWindow.KeyboardState.IsKeyDown(key) && GameWindow.KeyboardState.WasKeyDown(key);
}
