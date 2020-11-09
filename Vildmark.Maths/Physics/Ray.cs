﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Maths.Physics
{
    public struct Ray
    {
        public Vector3 Start;
        public Vector3 Direction;

        public Ray(Vector3 start, Vector3 direction)
        {
            Start = start;
            Direction = direction.Normalized();
        }

        public Ray Offset(Vector3 offset)
        {
            return new Ray(Start + offset, Direction);
        }
    }
}
