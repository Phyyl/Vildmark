using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Materials
{
    public class ModelMaterial : ColorMaterial
    {
        public Texture2D Texture { get; set; }

        public Vector3 Offset { get; set; }

        public ModelMaterial(Texture2D texture)
            : this(texture, Vector4.One)
        {
        }

        public ModelMaterial(Texture2D texture, Vector4 color)
            : base(color)
        {
            Texture = texture;
        }

        public override void SetupShader(IShader shader)
        {
            base.SetupShader(shader);

            if (shader is ISourceRectShader sourceRectShader)
            {
                sourceRectShader.SourceRect.SetValue(Texture.SourceRectangle.ToVector());
            }

            if (shader is ITextureShader textureShader)
            {
                textureShader.Texture.SetValue(Texture.GLTexture);
            }

            if (shader is IOffsetShader offsetShader)
            {
                offsetShader.Offset.SetValue(Offset);
            }
        }

        public static implicit operator ModelMaterial(Texture2D texture) => new(texture);
        public static implicit operator ModelMaterial((Vector4 tint, Texture2D texture) parameters) => new ModelMaterial(parameters.texture, parameters.tint);
        public static implicit operator ModelMaterial((Texture2D texture, Vector4 tint) parameters) => new ModelMaterial(parameters.texture, parameters.tint);
    }
}
