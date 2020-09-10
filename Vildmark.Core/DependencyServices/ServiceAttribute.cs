using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.DependencyServices
{
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(Type type = default, int priority = 0)
        {
            Type = type;
            Priority = priority;
        }

        public ServiceAttribute(int priority)
            : this(null, priority)
        {

        }

        public Type Type { get; }

        public int Priority { get; }
    }
}
