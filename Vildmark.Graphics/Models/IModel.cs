using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Models
{
    public interface IModel
    {
        Transform Transform { get; set; }

        void Render(RenderContext renderContext);
    }
}
