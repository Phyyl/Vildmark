using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Rendering;
using Vildmark.Input;

namespace Vildmark;

public abstract partial class VildmarkGame
{
    public Renderer Renderer { get; } = new();
    public Mouse Mouse { get; } = new();
    public Keyboard Keyboard { get; } = new();

    protected AutomaticOrthographicOffCenterCamera Automatic2DCamera { get; } = new();

    protected virtual void Load() { }
    protected virtual void Render(float delta) { }
    protected virtual void Update(float delta) { }
    protected virtual void Resize(int width, int height) { }
    protected virtual bool ShouldClose() => true;
    protected virtual void Unload() { }

    public static void Run<T>(WindowSettings? windowSettings = default)
        where T : VildmarkGame, new()
    {
        InitializeWindow(windowSettings ?? new());

        Window.Load += () =>
        {
            T game = new();

            game.Load();

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
}
