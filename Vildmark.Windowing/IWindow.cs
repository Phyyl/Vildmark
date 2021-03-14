using OpenTK.Mathematics;

namespace Vildmark.Windowing
{
    public interface IWindow
	{
		int Width { get; }

		int Height { get; }

		Vector2i Size { get; }

		IWindowHandler WindowHandler { get; set; }

		void Run();
        void Close();
    }
}
