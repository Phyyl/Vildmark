using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class TextureDictionary
    {
        private readonly Dictionary<string, RectangleF> rectangles = new Dictionary<string, RectangleF>();

        public GLTexture2D Texture { get; }

        public TextureDictionary(GLTexture2D texture, params (string name, RectangleF rectangle)[] rectangles)
        {
            Texture = texture;

            foreach (var rectangle in rectangles)
            {
                Add(rectangle.name, rectangle.rectangle);
            }
        }

        public Texture2D this[string name] => rectangles.TryGetValue(name, out RectangleF rectangle) ? new Texture2D(Texture, rectangle) : null;

        public bool Add(string name, RectangleF rectangle)
        {
            return rectangles.TryAdd(name, rectangle);
        }
    }
}
