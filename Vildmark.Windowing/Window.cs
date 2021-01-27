using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Windowing
{
    public partial class Window : IWindow
    {
        private readonly GameWindow gameWindow;
        private readonly WindowSettings settings;

        public IWindowHandler WindowHandler { get; set; }

        public int Width => Size.X;

        public int Height => Size.Y;

        public Vector2i Size { get; private set; }

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

            gameWindow.Load += GameWindow_Load;
            gameWindow.Unload += GameWindow_Unload;
            gameWindow.Resize += GameWindow_Resize;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            WindowHandler = windowHandler;

            this.settings = settings;
        }

        public void Run()
        {
            gameWindow.Run();
        }

        public void Close()
        {
            gameWindow.Close();
        }

        private void GameWindow_Load()
        {
            gameWindow.MakeCurrent();

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
        }

        private void GameWindow_UpdateFrame(FrameEventArgs obj)
        {
            WindowHandler?.Update((float)obj.Time);
        }

        private void GameWindow_RenderFrame(FrameEventArgs obj)
        {
            GL.Viewport(0, 0, Width, Height);

            WindowHandler?.Render((float)obj.Time);

            gameWindow.SwapBuffers();
        }
    }
}
