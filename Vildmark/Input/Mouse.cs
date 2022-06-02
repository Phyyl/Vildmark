using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Input;

public class Mouse : InputDevice
{
    public Vector2 Delta => GameWindow.MouseState.Delta;
    public Vector2 Position => GameWindow.MouseState.Position;
    public Vector2 Wheel => GameWindow.MouseState.ScrollDelta;

    internal Mouse()
    {
    }

    public bool IsMouseDown(MouseButton mouseButton)
    {
        return GameWindow.IsMouseButtonDown(mouseButton);
    }

    public bool IsMousePressed(MouseButton mouseButton)
    {
        return GameWindow.IsMouseButtonPressed(mouseButton);
    }

    public bool IsMouseReleased(MouseButton mouseButton)
    {
        return GameWindow.IsMouseButtonReleased(mouseButton);
    }

    public bool IsMouseUp(MouseButton mouseButton)
    {
        return !GameWindow.IsMouseButtonDown(mouseButton);
    }
}
