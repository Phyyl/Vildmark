using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Models
{
    public interface IModel
    {
        Transform Transform { get; set; }

        void Render(RenderContext renderContext, PrimitiveType primitiveType = PrimitiveType.Triangles);
    }
}
