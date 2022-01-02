using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Rendering;
using Vildmark.Maths;
using Vildmark.Windowing;

namespace Vildmark
{
    public abstract class VildmarkGame : IWindowHandler
    {
        public Window Window { get; }

        public IGamePad GamePad { get; }

        public IMouse Mouse { get; }

        public IKeyboard Keyboard { get; }

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

        protected virtual IKeyboard InitializeKeyboard() => Window;
        protected virtual IMouse InitializeMouse() => Window;
        protected virtual IGamePad InitializeGamePad() => Window;

        protected virtual void InitializeWindowSettings(WindowSettings settings)
        {
        }

        protected RenderContext Create2DRenderContext(bool offcenter = true, float zNear = 1, float zFar = -1)
        {
            return CreateRenderContext(offcenter ?
                new OrthographicOffCenterCamera(Window.Width, Window.Height, zNear, zFar) :
                new OrthographicCamera(Window.Width, Window.Height, zNear, zFar));
        }

        protected RenderContext Create3DRenderContext(float fovY = MathF.PI / 3f, float zNear = 0.01f, float zFar = 1000)
        {
            return CreateRenderContext(new PerspectiveCamera(Window.Width, Window.Height, fovY, zNear, zFar));
        }

        private RenderContext CreateRenderContext(Camera camera)
        {
            Window.OnResize += camera.Resize;

            return new RenderContext(camera);
        }

        public static void Run<T>()
            where T : VildmarkGame, new()
        {
            new T().Run();
        }
    }
}
