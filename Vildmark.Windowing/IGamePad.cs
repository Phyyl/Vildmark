﻿using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
    public interface IGamePad
    {
        float GetLeftTrigger(int index = 0);
        float GetRightTrigger(int index = 0);

        Vector2 GetLeftThumbStick(int index = 0);
        Vector2 GetRightThumbStick(int index = 0);

        bool IsGamePadButtonPressed(int button, int index = 0);
    }
}
