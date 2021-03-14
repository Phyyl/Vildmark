using OpenTK.Mathematics;

namespace Vildmark.Graphics.Resources
{
    public class ColorMaterial
    {
        public Vector4 Tint { get; init; }

        public ColorMaterial()
            : this(Vector4.One)
        {
        }

        public ColorMaterial(Vector4 tint)
        {
            Tint = tint;
        }

        public static implicit operator ColorMaterial(Vector4 tint) => new(tint);
    }
}
