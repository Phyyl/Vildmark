using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Rendering
{
    public abstract class RenderContext
    {
        private ModelShader modelShader;

        public FrameBuffer FrameBuffer { get; private set; }

        public Color4 ClearColor { get; set; } = Color4.CornflowerBlue;

        public abstract ICamera Camera { get; }

        public RenderContext()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void Resize(int width, int height)
        {
            Camera.Width = width;
            Camera.Height = height;
        }

        public void Clear()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }

        public void EnableDepthTest()
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public void DisableDepthTest()
        {
            GL.Disable(EnableCap.DepthTest);
        }

        public virtual void Begin(FrameBuffer frameBuffer = default, bool clear = true)
        {
            modelShader ??= new();

            FrameBuffer = frameBuffer;

            if (FrameBuffer is not null)
            {
                FrameBuffer.Bind();
            }
            else
            {
                GL.Viewport(0, 0, Camera.Width, Camera.Height);
            }

            if (clear)
            {
                Clear();
            }
        }

        public virtual void End()
        {
            if (FrameBuffer is not null)
            {
                FrameBuffer.Unbind();
            }
        }

        public void Render(IModel model, IShader shader = default)
        {
            shader ??= modelShader;

            model.Render(shader, Camera);
        }
    }
}
