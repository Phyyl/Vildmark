﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Vildmark.Resources
{
    public interface IResourceLoader<T> where T : class
    {
        T Load(Stream stream);
    }
}
