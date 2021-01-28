using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Vildmark;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Resources;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Resources
{
    [Register(typeof(IResourceLoader<Font>))]
    public class FontResourceLoader : IEmbeddedResourceLoader<Font>
    {
        public Font Load(string name, Assembly assembly = default)
        {
            assembly ??= Assembly.GetCallingAssembly();

            string json = ResourceLoader.LoadEmbedded<string>($"{name}.json", assembly);

            if (json is null)
            {
                return default;
            }

            Font font = JsonConvert.DeserializeObject<Font>(json);

            if (font is null)
            {
                return default;
            }

            font.Texture = ResourceLoader.LoadEmbedded<GLTexture2D>($"{name}.png");

            using (font.Texture.Bind())
            {
                font.Texture.TextureMagFilter = TextureMagFilter.Linear;
                font.Texture.TextureMinFilter = TextureMinFilter.Linear;
            }

            foreach (FontChar fontChar in font.Characters.Values)
            {
                fontChar.Texture = new Texture2D(font.Texture, new RectangleF(fontChar.X / font.Width, fontChar.Y / font.Height, fontChar.Width / font.Width, fontChar.Height / font.Height));
            }

            return font;
        }
    }
}
