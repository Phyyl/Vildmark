using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Rendering;
using Vildmark.Windowing;

namespace Vildmark
{
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

        protected RenderContext Create2DRenderContext(float zNear = 1, float zFar = -1)
        {
            OrthographicCamera camera = new(Window.Width, Window.Height, zNear, zFar);
            RenderContext renderContext = new(camera, Window);

            renderContext.OnBegin += (width, height) => (camera.Right, camera.Bottom) = (width, height);

            return renderContext;
        }

        protected RenderContext Create3DRenderContext(float fovY = MathF.PI / 3f, float zNear = 0.01f, float zFar = 1000)
        {
            PerspectiveCamera camera = new(Window.Width / (float)Window.Height, fovY, zNear, zFar);
            RenderContext renderContext = new(camera, Window);

            renderContext.OnBegin += (width, height) => camera.AspectRatio = width / (float)height;

            return renderContext;
        }

        public static void Run<T>()
            where T : VildmarkGame, new()
        {
            T instance = new T();

            instance.Run();
        }
    }
}
