namespace Vildmark.Helpers;

public static class GeometryHelper
{
    public static Box2 Fit(Box2 target, Box2 box)
    {
        float targetAspect = target.Size.X / target.Size.Y;
        float boxAspect = box.Size.X / box.Size.Y;

        if (boxAspect > targetAspect)
        {
            float height = target.Size.X / boxAspect;
            float width = target.Size.X;
            float y = (target.Size.Y - height) / 2;

            return new Box2(0, y, width, y + height);
        }
        else
        {
            float height = target.Size.Y;
            float width = target.Size.Y * boxAspect;
            float x = (target.Size.X - width) / 2;

            return new Box2(x, 0, x + width, height);
        }
    }
}
