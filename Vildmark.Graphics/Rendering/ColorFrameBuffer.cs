using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class ColorFrameBuffer : FrameBuffer
    {
        public ColorFrameBuffer(int width, int height)
            : base(width, height, FramebufferAttachment.ColorAttachment0, TextureOptions.Nearest)
        {

        }

        public GLRenderbuffer GLRenderbuffer { get; private set; }

        protected override void InitializeDrawBuffer(int width, int height)
        {
            GLRenderbuffer = new GLRenderbuffer(width, height);

            GLFramebuffer.SetRenderbuffer(GLRenderbuffer, FramebufferAttachment.ColorAttachment0);
        }

        protected override void InitializeReadBuffer(int width, int height)
        {
        }
    }
}
