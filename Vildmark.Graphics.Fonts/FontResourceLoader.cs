using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Resources
{
    [Register(typeof(IResourceLoader<BitmapFont>))]
    public class FontResourceLoader : IResourceLoader<BitmapFont>
    {
        private JsonSerializerOptions serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public BitmapFont Load(Stream stream)
        {
            string json = Service<IResourceLoader<string>>.Instance.Load(stream);

            if (json is null)
            {
                return default;
            }

            FontDefinition fontDefinition = JsonSerializer.Deserialize<FontDefinition>(json, serializerOptions);

            if (fontDefinition is null)
            {
                return default;
            }

            //BitmapFont result = new BitmapFont
            //{

            //}

            //font.Texture = ResourceLoader.LoadEmbedded<GLTexture2D, TextureLoadOptions>($"{name}.png", TextureLoadOptions.Linear);

            //foreach (BitmapFontChar fontChar in font.Characters.Values)
            //{
            //    fontChar.Texture = new Texture2D(font.Texture, new RectangleF(fontChar.X / font.Width, fontChar.Y / font.Height, fontChar.Width / font.Width, fontChar.Height / font.Height));
            //}

            return null;
        }

        private class FontDefinition
        {
            public string[] Pages { get; set; }
            public CharDefinition[] Chars { get; set; }
            public KerningDefinition[] Kernings { get; set; }
            public InfoDefinition Info { get; set; }
            public CommonDefinition Common { get; set; }
        }

        private class InfoDefinition
        {
            public string Face { get; set; }
            public int Size { get; set; }
            public int Bold { get; set; }
            public int Italic { get; set; }
            public string Charset { get; set; }
            public int Unicode { get; set; }
            public int StretchH { get; set; }
            public int Smooth { get; set; }
            public int Aa { get; set; }
            public int[] Padding { get; set; }
            public int[] Spacing { get; set; }
        }

        private class CommonDefinition
        {
            public int LineHeight { get; set; }
            public int Base { get; set; }
            public int ScaleW { get; set; }
            public int ScaleH { get; set; }
            public int Pages { get; set; }
            public int Packed { get; set; }
            public int AlphaChnl { get; set; }
            public int RedChnl { get; set; }
            public int GreenChnl { get; set; }
            public int BlueChnl { get; set; }
        }

        private class CharDefinition
        {
            public int ID { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int XOffset { get; set; }
            public int YOffset { get; set; }
            public int XAdvance { get; set; }
            public int Page { get; set; }
            public int Chnl { get; set; }
        }

        private class KerningDefinition
        {
            int First { get; set; }
            int Second { get; set; }
            int Amount { get; set; }
        }
    }
}
