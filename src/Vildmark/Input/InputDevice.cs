using OpenTK.Windowing.Desktop;

namespace Vildmark.Input;

public abstract class InputDevice
{
    protected internal readonly GameWindow GameWindow;

    protected internal InputDevice()
    {
        GameWindow = VildmarkGame.Window;
    }
}
