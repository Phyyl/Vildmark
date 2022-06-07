using OpenTK.Windowing.Common;

namespace Vildmark;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class WindowSettingsAttribute : Attribute
{
    public string Title { get; init; } = "Game";

    public int Width { get; init; } = 1280;
    public int Height { get; init; } = 720;
    public int Samples { get; init; } = 0;

    public WindowState State { get; init; } = WindowState.Normal;
    public WindowBorder Border { get; init; } = WindowBorder.Resizable;
}
