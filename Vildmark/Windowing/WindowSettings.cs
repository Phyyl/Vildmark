using OpenTK.Windowing.Common;

namespace Vildmark.Windowing
{
    public class WindowSettings
    {
        public string Title { get; set; } = "Game";

        public int Width { get; set; } = 1280;

        public int Height { get; set; } = 720;

        public WindowState State { get; set; } = WindowState.Normal;

        public WindowBorder Border { get; set; } = WindowBorder.Resizable;

        public int Samples { get; set; } = 0;

        public double UpdateFrequency { get; set; }

        public double RenderFrequency { get; set; }
    }
}
