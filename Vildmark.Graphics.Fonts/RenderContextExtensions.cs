using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
    public static class RenderContextExtensions
    {
        private static TextShader shader;

        public static void RenderText(this RenderContext renderContext, TextModel model)
        {
            shader ??= new();

            renderContext.Render(model, shader);
        }
    }
}
