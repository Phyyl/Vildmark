using OpenTK.Mathematics;

namespace Vildmark.Graphics.Fonts
{
    public struct BitmapFontVertex
    {
        public Vector2 Position;
        public Vector2 TexCoord;
        public int PageIndex;

        public BitmapFontVertex(Vector2 position, Vector2 texCoord, int pageIndex)
        {
            Position = position;
            TexCoord = texCoord;
            PageIndex = pageIndex;
        }
    }
}
