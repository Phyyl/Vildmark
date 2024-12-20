using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Vildmark.Input;

public class GamePad(int index = 0)
{
    internal JoystickState? State => VildmarkGame.Window.JoystickStates[Index];

    public int Index { get; } = index;
    public bool IsConnected => State is not null;

    public Vector2 LeftThumbStick => new(GetAxisOrDefault(0), GetAxisOrDefault(1));
    public Vector2 RightThumbStick => new(GetAxisOrDefault(2), GetAxisOrDefault(3));

    public float LeftTrigger => GetAxisOrDefault(4);
    public float RightTrigger => GetAxisOrDefault(5);

    public bool IsButtonDown(int button) => GetButtonOrDefault(button);
    public bool IsButtonUp(int button) => !GetButtonOrDefault(button);
    public bool IsButtonPressed(int button) => GetButtonOrDefault(button) && !GetPreviousButtonOrDefault(button);
    public bool IsButtonReleased(int button) => !GetButtonOrDefault(button) && GetPreviousButtonOrDefault(button);

    private float GetAxisOrDefault(int index) => State?.GetAxis(index) ?? default;
    private bool GetButtonOrDefault(int index) => State?.IsButtonDown(index) ?? default;
    private bool GetPreviousButtonOrDefault(int index) => State?.IsButtonDown(index) ?? default;
}
