using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Rendering
{
    public class ColorFrameBuffer : FrameBuffer
    {
        public ColorFrameBuffer(int width, int height)
            : base(width, height, FramebufferAttachment.ColorAttachment0, Texture2DFormat.Texture2D)
        {

        }

        public GLRenderbuffer? GLRenderbuffer { get; private set; }

        protected override void InitializeDrawBuffer(int width, int height)
        {
            GLRenderbuffer = new GLRenderbuffer(width, height);

            GLFramebuffer.SetRenderbuffer(GLRenderbuffer, FramebufferAttachment.ColorAttachment0);
        }

        protected override void InitializeReadBuffer(int width, int height)
        {
        }

        public virtual void Render(RenderContext renderContext, IShader? shader = default)
        {
            renderContext.RenderRectangle(new System.Drawing.RectangleF(0, 0, Width, Height), GLTexture, new Transform() { OriginY = Height / 2f, RotationX = MathF.PI }, shader: shader);
        }
    }
}
