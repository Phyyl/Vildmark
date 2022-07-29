using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using System.Reflection;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Rendering;
using Vildmark.Input;

namespace Vildmark;

public abstract partial class VildmarkGame
{
    public Renderer Renderer { get; } = new();

    protected virtual void Load() { }
    protected virtual void Render(float delta) { }
    protected virtual void Update(float delta) { }
    protected virtual void Resize(int width, int height) { }
    protected virtual bool ShouldClose() => true;
    protected virtual void Unload() { }

    public static void Run<T>()
        where T : VildmarkGame, new()
    {
        InitializeWindow(typeof(T).GetCustomAttribute<WindowSettingsAttribute>() ?? new());

        Window.Load += () =>
        {
#if DEBUG
            DisplayDiagnostics();
#endif

            T game = new();

            game.Load();
            game.Resize(Width, Height);

            Window.Resize += e => game.Resize(e.Width, e.Height);
            Window.Closing += e => e.Cancel = !game.ShouldClose();
            Window.Unload += () => game.Unload();

            Window.UpdateFrame += e => game.Update((float)e.Time);
            Window.RenderFrame += e =>
            {
                GL.Viewport(0, 0, Width, Height);
                game.Render((float)e.Time);
                Window.SwapBuffers();
            };
        };

        Window.Run();
    }

    private static void DisplayDiagnostics()
    {
        Console.WriteLine($"OpenGL version: {GL.GetInteger(GetPName.MajorVersion)}.{GL.GetInteger(GetPName.MinorVersion)}");
        Console.WriteLine($"Maximum texture size: {GL.GetInteger(GetPName.MaxTextureSize)}x{GL.GetInteger(GetPName.MaxTextureSize)}");
    }
}
