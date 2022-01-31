using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Vildmark.Windowing
{
    public partial class Window : IWindow
    {
        private readonly GameWindow gameWindow;

        public event ResizeEventHandler? OnResize;

        public IWindowHandler WindowHandler { get; set; }

        public int Width => Size.X;
        public int Height => Size.Y;

        public bool IsFocused => gameWindow.IsFocused;

        public bool CursorVisible
        {
            get => gameWindow.CursorVisible;
            set => gameWindow.CursorVisible = value;
        }

        public Vector2i Size
        {
            get => gameWindow.ClientRectangle.Size;
            set => gameWindow.ClientRectangle = new Box2i(gameWindow.ClientRectangle.Min, gameWindow.ClientRectangle.Min + value);
        }

        public double UpdateFrequency
        {
            get => gameWindow.UpdateFrequency;
            set => gameWindow.UpdateFrequency = value;
        }

        public double RenderFrequency
        {
            get => gameWindow.RenderFrequency;
            set => gameWindow.RenderFrequency = value;
        }

        public Window(WindowSettings settings, IWindowHandler windowHandler)
        {
            NativeWindowSettings nativeSettings = NativeWindowSettings.Default;

            nativeSettings.Size = new Vector2i(settings.Width, settings.Height);
            nativeSettings.Title = settings.Title;
            nativeSettings.WindowState = settings.State;
            nativeSettings.WindowBorder = settings.Border;
            nativeSettings.NumberOfSamples = settings.Samples;
            nativeSettings.Flags = ContextFlags.ForwardCompatible;

            gameWindow = new GameWindow(GameWindowSettings.Default, nativeSettings);

            UpdateFrequency = settings.UpdateFrequency;
            RenderFrequency = settings.RenderFrequency;

            gameWindow.Load += GameWindow_Load;
            gameWindow.Unload += GameWindow_Unload;
            gameWindow.Resize += GameWindow_Resize;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.MouseWheel += GameWindow_MouseWheel;
            gameWindow.KeyDown += GameWindow_KeyDown;
            gameWindow.KeyUp += GameWindow_KeyUp;
            WindowHandler = windowHandler;
        }

        public void Run()
        {
            gameWindow.Run();
        }

        public void Close()
        {
            gameWindow.Close();
        }

        public void Focus()
        {
            gameWindow.Focus();
        }

        private void GameWindow_Load()
        {
            gameWindow.MakeCurrent();

            Size = new Vector2i(gameWindow.ClientSize.X, gameWindow.ClientSize.Y);

            WindowHandler?.Load();
        }

        private void GameWindow_Unload()
        {
            WindowHandler?.Unload();
        }

        private void GameWindow_Resize(ResizeEventArgs obj)
        {
            Size = new Vector2i(obj.Width, obj.Height);

            WindowHandler?.Resize(obj.Width, obj.Height);

            OnResize?.Invoke(obj.Width, obj.Height);
        }

        private void GameWindow_UpdateFrame(FrameEventArgs obj)
        {
            BeginUpdateMouse();
            WindowHandler?.Update((float)obj.Time);
            EndUpdateMouse();
        }

        private void GameWindow_RenderFrame(FrameEventArgs obj)
        {
            GL.Viewport(0, 0, Width, Height);

            WindowHandler?.Render((float)obj.Time);

            gameWindow.SwapBuffers();
        }
    }
}
