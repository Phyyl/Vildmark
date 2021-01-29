using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Logging;

namespace Vildmark.Graphics.Helpers
{
    public static class GLHelper
    {
        public static void LogError()
        {
            ErrorCode error = GL.GetError();

            if (error == ErrorCode.NoError)
            {
                return;
            }

            Service<ILogger>.Instance.Error($"OpenGL error: {error}");
        }
    }
}
