using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Input;

public class Keyboard : InputDevice
{
    public event Action<Keys>? OnKeyPressed;
    public event Action<Keys>? OnKeyReleased;
    public event Action<char>? OnTextInput;

    internal Keyboard()
    {
        GameWindow.KeyDown += e => OnKeyPressed?.Invoke(e.Key);
        GameWindow.KeyUp += e => OnKeyReleased?.Invoke(e.Key);
        GameWindow.TextInput += e => OnTextInput?.Invoke((char)e.Unicode);
        GameWindow.KeyDown += e =>
        {
            char? chr = e.Key switch
            {
                Keys.Enter => '\n',
                Keys.Backspace => '\b',
                Keys.Tab => '\t',
                _ => null
            };

            if (chr.HasValue)
            {
                OnTextInput?.Invoke(chr.Value);
            }
        };
    }

    public bool IsKeyDown(Keys key) => GameWindow.IsKeyDown(key);
    public bool IsKeyUp(Keys key) => !IsKeyDown(key);
    public bool IsKeyPressed(Keys key) => GameWindow.KeyboardState.IsKeyDown(key) && !GameWindow.KeyboardState.WasKeyDown(key);
    public bool IsKeyReleased(Keys key) => !GameWindow.KeyboardState.IsKeyDown(key) && GameWindow.KeyboardState.WasKeyDown(key);
}
