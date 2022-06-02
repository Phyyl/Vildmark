using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Vildmark;

public abstract partial class VildmarkGame
{
    private static GameWindow? window;
    internal static GameWindow Window => window ?? throw new InvalidOperationException($"Attempt to use {nameof(VildmarkGame)}.{nameof(Window)} before initialization");

    public static int Width => Size.X;
    public static int Height => Size.Y;
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


    private static void InitializeWindow(WindowSettings settings)
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

    public record WindowSettings
    {
        public string Title { get; init; } = "Game";

        public int Width { get; init; } = 1280;
        public int Height { get; init; } = 720;
        public int Samples { get; init; } = 0;

        public WindowState State { get; init; } = WindowState.Normal;
        public WindowBorder Border { get; init; } = WindowBorder.Resizable;

        public double UpdateFrequency { get; init; }
        public double RenderFrequency { get; init; }
    }
}
