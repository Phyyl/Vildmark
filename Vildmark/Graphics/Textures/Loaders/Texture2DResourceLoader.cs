using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Textures.Loaders;

internal class Texture2DResourceLoader : IResourceLoader<Texture2D>
{
    public Texture2D Load(string name, ResourceLoadContext context)
    {
        return context.Load<GLTexture2D>(name);
    }
}
