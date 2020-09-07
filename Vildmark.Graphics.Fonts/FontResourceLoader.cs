using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using Vildmark.DependencyServices;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Resources;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Resources
{
    [DependencyService(typeof(IResourceLoader<string, Font>))]
    public class FontResourceLoader : IResourceLoader<string, Font>
    {
        public Font Load(string name, Assembly assembly)
        {
            string json = EmbeddedResources.Get<string>($"{name}.json", assembly);

            if (json is null)
            {
                return default;
            }

            Font font = JsonConvert.DeserializeObject<Font>(json);

            if (font is null)
            {
                return default;
            }

            font.Texture = EmbeddedResources.Get<GLTexture2D>($"{name}.png", assembly);

            using (font.Texture.Bind())
            {
                font.Texture.TextureMagFilter = TextureMagFilter.Linear;
                font.Texture.TextureMinFilter = TextureMinFilter.Linear;
            }

            return font;
        }
    }
}
