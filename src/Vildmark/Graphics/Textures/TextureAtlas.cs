namespace Vildmark.Graphics.Textures;

public class TextureAtlas
{
    public Vector2 TexelTileSize { get; }
    public Vector2 TexelTileSpacing { get; }

    public Vector2i TileSize { get; }
    public Vector2i TileSpacing { get; }

    public Texture2D Texture { get; }

    public TextureAtlas(Texture2D texture, int tileWidth, int tileHeight, int xSpacing = 0, int ySpacing = 0)
    {
        Texture = texture;
        TileSize = new(tileWidth, tileHeight);
        TileSpacing = new Vector2i(xSpacing, ySpacing);

        TexelTileSize = TileSize / texture.Size;
        TexelTileSpacing = TileSpacing / texture.Size;
    }

    public SubTexture2D this[int x, int y]
    {
        get
        {
            Vector2 min = new Vector2(x, y) * (TexelTileSize + TexelTileSpacing);

            return new SubTexture2D(Texture, new Box2(min, min + TexelTileSize));
        }
    }
}
