﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Resources
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterAttribute : Attribute
    {
        public RegisterAttribute(Type type, int priority = 0)
        {
            Type = type;
            Priority = priority;
        }

        public Type Type { get; }

        public int Priority { get; }
    }
}
