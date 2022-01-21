using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class DepthFrameBuffer : FrameBuffer
    {
        public DepthFrameBuffer(int width, int height)
            : base(width, height, FramebufferAttachment.DepthAttachment, Texture2DFormat.Depth)
        {
        }

        protected override void InitializeDrawBuffer(int width, int height)
        {
            GLFramebuffer.DrawBuffer(DrawBufferMode.None);
        }

        protected override void InitializeReadBuffer(int width, int height)
        {
            GLFramebuffer.ReadBuffer(ReadBufferMode.None);
        }
    }
}
