using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Helpers;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext2D : RenderContext
    {
        public OrthographicOffCenterCamera OrthographicCamera { get; }

        public override Camera Camera => OrthographicCamera;

        public RenderContext2D(int width, int height)
        {
            OrthographicCamera = new OrthographicOffCenterCamera(width, height);
        }
    }
}
