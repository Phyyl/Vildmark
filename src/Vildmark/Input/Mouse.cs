using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Input;

public static class Mouse
{
    public static Vector2 Delta => VildmarkGame.Window.MouseState.Delta;
    public static Vector2 Position => VildmarkGame.Window.MouseState.Position;
    public static Vector2 Wheel => VildmarkGame.Window.MouseState.ScrollDelta;

    public static bool IsMouseDown(MouseButton mouseButton) => VildmarkGame.Window.IsMouseButtonDown(mouseButton);
    public static bool IsMousePressed(MouseButton mouseButton) => VildmarkGame.Window.IsMouseButtonPressed(mouseButton);
    public static bool IsMouseReleased(MouseButton mouseButton) => VildmarkGame.Window.IsMouseButtonReleased(mouseButton);
    public static bool IsMouseUp(MouseButton mouseButton) => !VildmarkGame.Window.IsMouseButtonDown(mouseButton);
}
