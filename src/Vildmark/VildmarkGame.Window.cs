using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Vildmark;

public abstract partial class VildmarkGame
{
    private static GameWindow? window;
    internal static GameWindow Window => window ?? throw new InvalidOperationException($"Attempt to use {nameof(VildmarkGame)}.{nameof(Window)} before initialization");

    public static int Width => Size.X;
    public static int Height => Size.Y;
    public static float AspectRatio => Width / (float)Height;
    public static bool IsFocused => Window.IsFocused;

    public static CursorState CursorState
    {
        get => Window.CursorState;
        set => Window.CursorState = value;
    }

    public static Vector2i Size
    {
        get => Window.ClientRectangle.Size;
        set => Window.ClientRectangle = new Box2i(Window.ClientRectangle.Min, Window.ClientRectangle.Min + value);
    }

    public static Vector2 Center => Size / 2;

    public static double UpdateFrequency
    {
        get => Window.UpdateFrequency;
        set => Window.UpdateFrequency = value;
    }

    public static double RenderFrequency
    {
        get => Window.RenderFrequency;
        set => Window.RenderFrequency = value;
    }

    private static void InitializeWindow(WindowSettingsAttribute settings)
    {
        NativeWindowSettings nativeSettings = NativeWindowSettings.Default;

        nativeSettings.Size = new Vector2i(settings.Width, settings.Height);
        nativeSettings.Title = settings.Title;
        nativeSettings.WindowState = settings.State;
        nativeSettings.WindowBorder = settings.Border;
        nativeSettings.NumberOfSamples = settings.Samples;
        nativeSettings.Flags = ContextFlags.ForwardCompatible;

        window = new GameWindow(GameWindowSettings.Default, nativeSettings);
    }
}
