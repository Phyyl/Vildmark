using OpenTK.Graphics.OpenGL;
using Vildmark.Logging;

namespace Vildmark.Helpers;

public static class GLHelper
{
    public static void LogError()
    {
        ErrorCode error = GL.GetError();

        if (error == ErrorCode.NoError)
        {
            return;
        }

        Logger.Error($"OpenGL error: {error}");
    }
}
