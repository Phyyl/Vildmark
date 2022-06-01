using System.Drawing;
using System.Drawing.Imaging;
using Vildmark.Resources;

namespace Vildmark.Graphics.GLObjects.Loaders;

internal class GLTexture2DResourceLoader : IResourceLoader<GLTexture2D>
{
    public unsafe GLTexture2D Load(string name, ResourceLoadContext context)
    {
#pragma warning disable CA1416 // Validate platform compatibility
        Bitmap bitmap = new(context.GetStream(name));
        BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        Span<byte> span = new(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height);
        GLTexture2D texture = new(bitmap.Width, bitmap.Height, span);
        bitmap.UnlockBits(data);
#pragma warning restore CA1416 // Validate platform compatibility

        return texture;
    }
}
