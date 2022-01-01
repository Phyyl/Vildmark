using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
    public static class RenderContextExtensions
    {
        private static TextShader shader;
        private static TextModel model;

        public static void RenderText(this RenderContext renderContext, BitmapFont font, Vector2 position, string text, float size, Color4 color)
        {
            renderContext.EnableBlending();
            renderContext.DisableDepthTest();

            shader ??= new();

            if (model is null)
            {
                model = font.CreateModel(text, size, color);
            }
            else
            {
                font.UpdateModel(model, text, size, color);
            }

            model.Transform.Position = new Vector3(position);

            renderContext.Render(model);
        }
    }
}
