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
    public partial class Window // : IGamePad
    {
        //public bool IsGamePadConnected(int index = 0)
        //{
        //    //TODO: Was this removed from 9.9?
        //    return true;
        //}

        //public Vector2 GetLeftThumbStick(int index = 0)
        //{
        //    if (!IsGamePadConnected(index))
        //    {
        //        return default;
        //    }

        //    return new Vector2(gameWindow.JoystickStates[index].GetAxis(0), gameWindow.JoystickStates[index].GetAxis(1));
        //}

        //public Vector2 GetRightThumbStick(int index = 0)
        //{
        //    if (!IsGamePadConnected(index))
        //    {
        //        return default;
        //    }

        //    return new Vector2(gameWindow.JoystickStates[index].GetAxis(2), gameWindow.JoystickStates[index].GetAxis(3));
        //}

        //public bool IsGamePadButtonDown(int button, int index = 0)
        //{
        //    if (!IsGamePadConnected(index))
        //    {
        //        return default;
        //    }

        //    return gameWindow.JoystickStates[index].IsButtonDown(button);
        //}

        //public bool IsGamePadButtonUp(int button, int index = 0)
        //{
        //    if (!IsGamePadConnected(index))
        //    {
        //        return default;
        //    }

        //    return !IsGamePadButtonDown(button, index);
        //}

        //public bool IsGamePadButtonPressed(int button, int index = 0)
        //{
        //    if (!IsGamePadConnected(index))
        //    {
        //        return default;
        //    }

        //    return !gameWindow.LastJoystickStates[index].IsButtonDown(button) && IsGamePadButtonDown(button, index);
        //}

        //public bool IsGamePadButtonReleased(int button, int index = 0)
        //{
        //    if (!IsGamePadConnected(index))
        //    {
        //        return default;
        //    }

        //    return gameWindow.LastJoystickStates[index].IsButtonDown(button) && IsGamePadButtonUp(button, index);
        //}

        //public float GetLeftTrigger(int index = 0)
        //{
        //    return gameWindow.JoystickStates[index].GetAxis(4);
        //}

        //public float GetRightTrigger(int index = 0)
        //{
        //    return gameWindow.JoystickStates[index].GetAxis(5);
        //}
    }
}
