using OpenTK.Mathematics;
using System.Drawing;

namespace Vildmark.Graphics.Rendering
{
    public class TextureAtlas
    {
        public Vector2 TileSize { get; }

        public Texture2D Texture { get; }

        public TextureAtlas(Texture2D texture, int tileWidth, int tileHeight)
        {
            Texture = texture;
            TileSize = new Vector2(tileWidth, tileHeight);
        }

        public Texture2D this[int x, int y] => Texture.CreateSubTexture(new RectangleF(x / Texture.Width, y / Texture.Height, TileSize.X / Texture.Width, TileSize.Y / Texture.Height));
    }
}
