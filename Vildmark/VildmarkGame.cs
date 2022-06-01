using Vildmark.Windowing;

namespace Vildmark;

public abstract class VildmarkGame : IWindowHandler
{
    public Window Window { get; }

    public IGamePad GamePad { get; }

    public IMouse Mouse { get; }

    public IKeyboard Keyboard { get; }

    public double UpdateFrequency
    {
        get => Window.UpdateFrequency;
        set => Window.UpdateFrequency = value;
    }

    public double RenderFrequency
    {
        get => Window.RenderFrequency;
        set => Window.RenderFrequency = value;
    }

    protected VildmarkGame()
    {
        WindowSettings settings = new();

        InitializeWindowSettings(settings);

        Window = new Window(settings, this);
        GamePad = InitializeGamePad();
        Keyboard = InitializeKeyboard();
        Mouse = InitializeMouse();
    }

    public void Run()
    {
        Window.Run();
        Stop();
    }

    public void Stop()
    {
        Window.Close();
    }

    public virtual void Load()
    {
    }

    public virtual void Unload()
    {
    }

    public virtual void Resize(int width, int height)
    {
    }

    public virtual void Update(float delta)
    {
    }

    public virtual void Render(float delta)
    {
    }

    public virtual void Close()
    {

    }

    protected virtual IKeyboard InitializeKeyboard() => Window;
    protected virtual IMouse InitializeMouse() => Window;
    protected virtual IGamePad InitializeGamePad() => Window;

    protected virtual void InitializeWindowSettings(WindowSettings settings)
    {
    }

    public static void Run<T>()
        where T : VildmarkGame, new()
    {
        T instance = new();

        instance.Run();
    }
}
