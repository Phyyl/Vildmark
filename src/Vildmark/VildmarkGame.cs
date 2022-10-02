using OpenTK.Graphics.OpenGL4;
using System.Reflection;
using Vildmark.Graphics.Rendering;

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

            if (game is VildmarkGame<T> instanceGame)
            {
                VildmarkGame<T>.Instance = game;
            }

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

public abstract class VildmarkGame<T> : VildmarkGame
    where T : VildmarkGame
{
    private static T? instance;

    public static T Instance
    {
        get => instance ?? throw new InvalidOperationException("Game not yet initialized");
        internal set => instance ??= value;
    }
}
