using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics
{
    public interface IRenderable
    {
        void Render(RenderContext renderContext);
    }
}
