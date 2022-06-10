using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Input;

public static class Keyboard
{
    public static event Action<Keys>? OnKeyPressed;
    public static event Action<Keys>? OnKeyReleased;
    public static event Action<char>? OnTextInput;

    static Keyboard()
    {
        VildmarkGame.Window.KeyDown += e => OnKeyPressed?.Invoke(e.Key);
        VildmarkGame.Window.KeyUp += e => OnKeyReleased?.Invoke(e.Key);
        VildmarkGame.Window.TextInput += e => OnTextInput?.Invoke((char)e.Unicode);
        VildmarkGame.Window.KeyDown += e =>
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

    public static bool IsKeyDown(Keys key) => VildmarkGame.Window.IsKeyDown(key);
    public static bool IsKeyUp(Keys key) => !IsKeyDown(key);
    public static bool IsKeyPressed(Keys key) => VildmarkGame.Window.KeyboardState.IsKeyDown(key) && !VildmarkGame.Window.KeyboardState.WasKeyDown(key);
    public static bool IsKeyReleased(Keys key) => !VildmarkGame.Window.KeyboardState.IsKeyDown(key) && VildmarkGame.Window.KeyboardState.WasKeyDown(key);
}
