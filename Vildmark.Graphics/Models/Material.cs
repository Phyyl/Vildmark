using Vildmark.Graphics.GLObjects;
using OpenTK.Mathematics;
using Vildmark.Graphics.Resources;
using System.Drawing;
using Vildmark.Graphics.Rendering;
using System;

namespace Vildmark.Graphics.Models
{
    public class Material
    {
        public Texture2D Texture { get; }

        public Vector4 Tint { get; set; }

        public Material(Texture2D texture)
            : this(texture, Vector4.One)
        {

        }

        public Material(Vector4 tint)
            : this(Textures.WhitePixel, tint)
        {
        }

        public Material(Texture2D texture, Vector4 tint)
        {
            Texture = texture;
            Tint = tint;
        }

        public void Setup(MaterialShader shader)
        {
            shader.Tint.SetValue(Tint);
            shader.Tex0.SetValue(Texture?.GLTexture);
        }
    }
}