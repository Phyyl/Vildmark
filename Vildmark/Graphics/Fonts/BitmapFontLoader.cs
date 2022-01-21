using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Logging;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Resources
{
    [Register(typeof(IResourceLoader<BitmapFont>))]
    public class BitmapFontLoader : IResourceLoader<BitmapFont>
    {
        private readonly JsonSerializerOptions serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public BitmapFont? Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            string? json = ResourceLoader.Load<string>(stream);

            if (json is null)
            {
                return default;
            }

            FontDefinition? definition = JsonSerializer.Deserialize<FontDefinition>(json, serializerOptions);

            if (definition is null)
            {
                Logger.Warning($"Could not deserialize font definition ({resourceName})");
                return default;
            }

            if (definition.Common is null)
            {
                Logger.Warning($"Invalid common section in font definition ({resourceName})");
                return default;
            }

            if (definition.Info is null)
            {
                Logger.Warning($"Invalid info section in font definition ({resourceName})");
                return default;
            }

            if (definition.Info.Face is null)
            {
                Logger.Warning($"Invalid font name in font info definition ({resourceName})");
                return default;
            }

            if (definition.Pages is null)
            {
                Logger.Warning($"Invalid pages in font definition ({resourceName})");
                return default;
            }

            if (definition.Chars is null)
            {
                Logger.Warning($"Invalid chars in font definition ({resourceName})");
                return default;
            }

            if (definition.Common is null)
            {
                Logger.Warning($"Invalid common section in font definition ({resourceName})");
                return default;
            }

            Texture2D[] pages = new Texture2D[definition.Pages.Length];

            for (int i = 0; i < definition.Pages.Length; i++)
            {
                string page = definition.Pages[i];

                GLTexture2D? texture = ResourceLoader.LoadEmbedded<GLTexture2D, Texture2DMode>(page, Texture2DMode.Linear, assembly);

                if (texture is null)
                {
                    Logger.Warning($"Could not load font page texture ({resourceName}, {page})");
                    return default;
                }

                pages[i] = texture;
            }

            BitmapFontChar[]? chars = definition.Chars.Select(d => new BitmapFontChar((char)d.ID, d.X, d.Y, d.Width, d.Height, d.XOffset, d.YOffset, d.XAdvance, d.Page)).ToArray();

            return new(chars)
            {
                Name = definition.Info.Face,
                Pages = pages,
                LineHeight = definition.Common.LineHeight,
                Size = Math.Abs(definition.Info.Size),
                Base = definition.Common.Base
            };
        }

        private class FontDefinition
        {
            public string[]? Pages { get; set; }
            public CharDefinition[]? Chars { get; set; }
            public KerningDefinition[]? Kernings { get; set; }
            public InfoDefinition? Info { get; set; }
            public CommonDefinition? Common { get; set; }
        }

        private class InfoDefinition
        {
            public string? Face { get; set; }
            public int Size { get; set; }
            public int Bold { get; set; }
            public int Italic { get; set; }
            public string? Charset { get; set; }
            public int Unicode { get; set; }
            public int StretchH { get; set; }
            public int Smooth { get; set; }
            public int Aa { get; set; }
            public int[]? Padding { get; set; }
            public int[]? Spacing { get; set; }
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
            public int First { get; set; }
            public int Second { get; set; }
            public int Amount { get; set; }
        }
    }
}
