using OpenTK.Mathematics;

namespace Vildmark.Windowing
{
    public delegate void ResizeEventHandler(int width, int height);

    public interface IWindow
	{
        event ResizeEventHandler? OnResize;

		int Width { get; }

		int Height { get; }

		Vector2i Size { get; set; }

		IWindowHandler WindowHandler { get; set; }

		void Run();

        void Close();
    }
}
