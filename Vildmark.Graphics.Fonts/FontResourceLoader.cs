using Newtonsoft.Json;
using System.Drawing;
using System.Reflection;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Resources
{
    //[Register(typeof(IResourceLoader<Font>))]
    //public class FontResourceLoader : IResourceLoader<Font>
    //{
    //    public Font Load(string name, Assembly assembly = default)
    //    {
    //        assembly ??= Assembly.GetCallingAssembly();

    //        string json = ResourceLoader.LoadEmbedded<string>($"{name}.json", assembly);

    //        if (json is null)
    //        {
    //            return default;
    //        }

    //        Font font = JsonConvert.DeserializeObject<Font>(json);

    //        if (font is null)
    //        {
    //            return default;
    //        }

    //        font.Texture = ResourceLoader.LoadEmbedded<GLTexture2D, TextureLoadOptions>($"{name}.png", TextureLoadOptions.Linear);

    //        foreach (FontChar fontChar in font.Characters.Values)
    //        {
    //            fontChar.Texture = new Texture2D(font.Texture, new RectangleF(fontChar.X / font.Width, fontChar.Y / font.Height, fontChar.Width / font.Width, fontChar.Height / font.Height));
    //        }

    //        return font;
    //    }
    //}
}
