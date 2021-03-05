using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vildmark.Windowing
{
    public partial class Window : IGamePad
    {
        public bool IsGamePadConnected(int index = 0) => gameWindow.JoystickStates[index] != null;

        public Vector2 GetLeftThumbStick(int index = 0) => new(gameWindow.JoystickStates[index]?.GetAxis(0) ?? default, gameWindow.JoystickStates[index]?.GetAxis(1) ?? default);

        public Vector2 GetRightThumbStick(int index = 0) => new(gameWindow.JoystickStates[index]?.GetAxis(2) ?? default, gameWindow.JoystickStates[index]?.GetAxis(3) ?? default);

        public bool IsGamePadButtonDown(int button, int index = 0) => gameWindow.JoystickStates[index]?.IsButtonDown(button) ?? default;

        public bool IsGamePadButtonUp(int button, int index = 0) => !IsGamePadButtonDown(button, index);

        public bool IsGamePadButtonPressed(int button, int index = 0) => !(gameWindow.JoystickStates[index]?.WasButtonDown(button) ?? default) && IsGamePadButtonDown(button, index);

        public bool IsGamePadButtonReleased(int button, int index = 0) => (gameWindow.JoystickStates[index]?.WasButtonDown(button) ?? default) && IsGamePadButtonUp(button, index);

        public float GetLeftTrigger(int index = 0) => gameWindow.JoystickStates[index]?.GetAxis(4) ?? default;

        public float GetRightTrigger(int index = 0) => gameWindow.JoystickStates[index]?.GetAxis(5) ?? default;
    }
}
