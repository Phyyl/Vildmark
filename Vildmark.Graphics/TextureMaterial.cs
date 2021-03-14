using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Resources
{
    public class TextureMaterial : ColorMaterial
    {
        public Texture2D Texture { get; init; }

        public TextureMaterial(Texture2D texture)
            : this(texture, Vector4.One)
        {

        }

        public TextureMaterial(Texture2D texture, Vector4 color)
            : base(color)
        {
            Texture = texture;
        }

        public static implicit operator TextureMaterial(Texture2D texture) => new(texture);
        public static implicit operator TextureMaterial((Vector4 tint, Texture2D texture) parameters) => new TextureMaterial(parameters.texture, parameters.tint);
        public static implicit operator TextureMaterial((Texture2D texture, Vector4 tint) parameters) => new TextureMaterial(parameters.texture, parameters.tint);
    }
}
