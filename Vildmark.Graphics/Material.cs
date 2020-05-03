using Vildmark.Graphics.GLObjects;
using OpenToolkit.Mathematics;

namespace Vildmark.Rendering
{
    internal class Material
    {
        public Material(GLTexture2D texture, Vector4 color)
        {
            GLTexture = texture;
            Color = color;
        }

        public GLTexture2D GLTexture { get; }
        
        public Vector4 Color { get; }
    }
}