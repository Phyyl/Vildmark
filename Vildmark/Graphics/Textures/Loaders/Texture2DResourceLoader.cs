using System.Drawing.Imaging;
using System.Drawing;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Textures.Loaders;

internal class Texture2DResourceLoader : IResourceLoader<Texture2D>
{
    public unsafe Texture2D Load(string name, ResourceLoadContext context)
    {
        Bitmap bitmap = new(context.GetStream(name));
        BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        Span<byte> span = new(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height);
        GLTexture2D texture = new(bitmap.Width, bitmap.Height, span);
        bitmap.UnlockBits(data);
        return new Texture2D(texture);
    }
}
