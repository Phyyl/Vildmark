using OpenTK.Mathematics;

namespace Vildmark.Graphics.Textures;

public class TextureDictionary(Texture2D texture, params TextureDictionary<string>.Entry[] entries) : TextureDictionary<string>(texture, entries);

public class TextureDictionary<TKey>(Texture2D texture, params TextureDictionary<TKey>.Entry[] entries)
    where TKey : notnull
{
    private readonly Dictionary<TKey, SubTexture2D> rectangles = entries.ToDictionary(
        e => e.Key,
        e => new SubTexture2D(
            texture,
            new Box2(
                e.Region.Min.X / texture.Width,
                e.Region.Min.Y / texture.Height,
                e.Region.Max.X / texture.Width,
                e.Region.Max.Y / texture.Height)));

    internal Texture2D Texture { get; } = texture;

    public Texture2D? this[TKey key] => rectangles.GetValueOrDefault(key);

    public record Entry(TKey Key, Box2 Region);
}
