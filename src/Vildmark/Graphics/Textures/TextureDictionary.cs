using OpenTK.Mathematics;

namespace Vildmark.Graphics.Textures;

public class TextureDictionary<TKey>
    where TKey : notnull
{
    private readonly Dictionary<TKey, SubTexture2D> rectangles = new();

    internal Texture2D Texture { get; }

    public TextureDictionary(Texture2D texture, params Entry[] entries)
    {
        Texture = texture;
        rectangles = entries.ToDictionary(e => e.Key, e => new SubTexture2D(texture, e.SourceRectangle));
    }

    public Texture2D? this[TKey key] => rectangles.GetValueOrDefault(key);

    public record Entry(TKey Key, Box2 SourceRectangle);
}
