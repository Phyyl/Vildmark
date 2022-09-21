using Vildmark.Graphics.Textures;
using Vildmark.Graphics.Textures.Loaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Loaders;

internal class MsdfFontResourceLoader : IResourceLoader<Font>
{
    public Font Load(string name, ResourceLoadContext context)
    {
        FontInfo info = context.Load<FontInfo>($"{name}.json");
        Texture2D texture = context.Load<Texture2D, Texture2DOptions>($"{name}.png", Texture2DOptions.Linear);

        return new Font(info, texture);
    }
}
