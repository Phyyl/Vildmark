using OpenTK.Mathematics;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Materials
{
    public class ColorMaterial : IMaterial
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

        public virtual void SetupShader(IShader shader)
        {
            if (shader is ITintShader tintShader)
            {
                tintShader.Tint.SetValue(Tint);
            }
        }
    }
}
