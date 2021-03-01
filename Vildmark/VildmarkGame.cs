using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Windowing;

namespace Vildmark
{
    public abstract class VildmarkGame : IWindowHandler
    {
        private Window window;

        public IWindow Window { get; }

        public IGamePad GamePad { get; }

        public IMouse Mouse { get; }

        public IKeyboard Keyboard { get; }

        protected VildmarkGame()
        {
            WindowSettings settings = new WindowSettings();

            InitializeWindowSettings(settings);

            Window = window = new Window(settings, this);
            GamePad = InitializeGamePad();
            Keyboard = InitializeKeyboard();
            Mouse = InitializeMouse();
        }

        public void Run()
        {
            window.Run();
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

        protected virtual IKeyboard InitializeKeyboard() => window;
        protected virtual IMouse InitializeMouse() => window;
        protected virtual IGamePad InitializeGamePad() => window;

        protected virtual void InitializeWindowSettings(WindowSettings settings)
        {
        }
    }
}
