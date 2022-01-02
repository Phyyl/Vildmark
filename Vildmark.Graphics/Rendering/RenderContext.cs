using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Rendering
{
    public partial class RenderContext
    {
        private FrameBuffer? frameBuffer;

        public Color4 ClearColor { get; set; } = Color4.Black;

        public Camera Camera { get; }

        public bool DepthTest
        {
            get => GL.IsEnabled(EnableCap.DepthTest);
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.DepthTest);
                }
                else
                {
                    GL.Disable(EnableCap.DepthTest);
                }
            }
        }

        public bool Blending
        {
            get => GL.IsEnabled(EnableCap.DepthTest);
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                }
                else
                {
                    GL.Disable(EnableCap.Blend);
                }
            }
        }

        public RenderContext(Camera camera)
        {
            Camera = camera;
        }

        public virtual void Resize(int width, int height)
        {
            Camera.Width = width;
            Camera.Height = height;
        }

        public virtual void Clear()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }

        public virtual void Begin(FrameBuffer? frameBuffer = default, bool clear = true)
        {
            this.frameBuffer = frameBuffer;

            if (this.frameBuffer is not null)
            {
                this.frameBuffer.Bind();
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
            if (frameBuffer is not null)
            {
                frameBuffer.Unbind();
            }
        }
    }
}
