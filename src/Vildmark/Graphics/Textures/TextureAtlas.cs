using OpenTK.Mathematics;

namespace Vildmark.Graphics.Textures;

public class TextureAtlas
{
    public Vector2 TileSize { get; }

    public Texture2D Texture { get; }

    public TextureAtlas(Texture2D texture, int tileWidth, int tileHeight)
    {
        Texture = texture;
        TileSize = new Vector2(tileWidth, tileHeight) / texture.Size;
    }

    public Texture2D this[int x, int y]
    {
        get
        {
            Vector2 min = new Vector2(x, y) * TileSize;

            return new SubTexture2D(Texture, new Box2(min, min + TileSize));
        }
    }
}
