using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext3D : RenderContext
    {
        public PerspectiveCamera PerspectiveCamera { get; }

        public override Camera Camera => PerspectiveCamera;

        public RenderContext3D(int width, int height, float fovY)
        {
            PerspectiveCamera = new PerspectiveCamera(fovY, width, height);
        }

        public override IDisposable Begin()
        {
            EnableDepthTest();

            return base.Begin();
        }
    }
}
