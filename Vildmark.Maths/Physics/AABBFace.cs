﻿using System;

namespace Vildmark.Maths.Physics
{
    [Flags]
    public enum AABBFace
    {
        None = 0,
        Left = 1,
        Right = 2,
        Bottom = 4,
        Top = 8,
        Back = 16,
        Front = 32,
        All = Left | Right | Bottom | Top | Back | Front
    }
}
