using OpenTK.Mathematics;

namespace Vildmark.Graphics.Textures;

public class TextureDictionary<TKey>(Texture2D texture, params TextureDictionary<TKey>.Entry[] entries)
    where TKey : notnull
{
    private readonly Dictionary<TKey, SubTexture2D> rectangles = entries.ToDictionary(e => e.Key, e => new SubTexture2D(texture, e.SourceRectangle));

    internal Texture2D Texture { get; } = texture;

    public Texture2D? this[TKey key] => rectangles.GetValueOrDefault(key);

    public record Entry(TKey Key, Box2 SourceRectangle);
}
