using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Windowing
{
    public interface IMouse
    {
        Vector2 Delta { get; }
        Vector2 Position { get; }
        Vector2 Wheel { get; }

        bool IsMouseGrabbed { get; set; }

        bool IsMouseDown(MouseButton mouseButton);
        bool IsMouseUp(MouseButton mouseButton);
        bool IsMousePressed(MouseButton mouseButton);
        bool IsMouseReleased(MouseButton mouseButton);
    }
}
