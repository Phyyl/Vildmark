using OpenTK.Windowing.Desktop;

namespace Vildmark.Windowing.Input;

public abstract class InputDevice
{
    protected internal readonly GameWindow GameWindow;

    protected internal InputDevice()
    {
        GameWindow = VildmarkGame.Window;
    }
}
