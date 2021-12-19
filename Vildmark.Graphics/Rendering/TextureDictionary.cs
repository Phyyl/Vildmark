using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class TextureDictionary<TKey>
    {
        private readonly Dictionary<TKey, Texture2D> rectangles = new();

        public GLTexture2D Texture { get; }

        public TextureDictionary(GLTexture2D texture, params Entry[] entries)
        {
            Texture = texture;
            rectangles = entries.ToDictionary(e => e.Key, e => new Texture2D(texture, e.SourceRectangle));
        }

        public Texture2D this[TKey key] => rectangles.GetValueOrDefault(key);

        public record Entry(TKey Key, RectangleF SourceRectangle);
    }
}
