using OpenTK.Graphics.OpenGL4;
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

            Logger.Error($"OpenGL error: {error}");
        }
    }
}
