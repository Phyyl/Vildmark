using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Shapes;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext2D : RenderContext
    {
        private readonly ICamera camera;
        private readonly RectangleModel rectangle;
        private readonly CircleModel circle;

        public override ICamera Camera => camera;


        private RenderContext2D(ICamera camera)
        {
            this.camera = camera;

            rectangle = new RectangleModel(1,1);
            circle = new CircleModel(1);
        }

        public void RenderRectangle(Vector2 position, Vector2 size, Vector4 color)
        {
            rectangle.Size = size;
            rectangle.Transform.X = position.X;
            rectangle.Transform.Y = position.Y;
            rectangle.Material.Tint = color;

            Render(rectangle);
        }

        public void RenderCicle(Vector2 position, float radius, Vector4 color)
        {
            circle.Transform.X = position.X;
            circle.Transform.Y = position.Y;
            circle.Radius = radius;
            circle.Material.Tint = color;

            Render(circle);
        }

        public static RenderContext2D Create(int width = 1920, int height = 1080, float zNear = 1, float zFar = -1)
        {
            return new RenderContext2D(new OrthographicOffCenterCamera(width, height, zNear, zFar));
        }
    }
}
