using System.Collections.Generic;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
    public record BitmapFontChar(char Character, int X, int Y, int Width, int Height, int XOffset, int YOffset, int XAdvance, int Page);

    public class BitmapFont
    {
        private readonly Dictionary<char, BitmapFontChar> characters = new Dictionary<char, BitmapFontChar>();

		public GLTexture2D[] Pages { get; internal init;  }

		public string Name { get; internal init; }

		public int Size { get; internal init; }

		public bool Bold { get; internal init; }

		public bool Italic { get; internal init; }

		public int Width { get; internal init; }

		public int Height { get; internal init; }

		public int BaseLine => (int)(Size * 0.75f);

        internal BitmapFont() { }

        public bool TryGetChar(char character, out BitmapFontChar bitmapChar)
        {
            return characters.TryGetValue(character, out bitmapChar);
        }
    }
}
