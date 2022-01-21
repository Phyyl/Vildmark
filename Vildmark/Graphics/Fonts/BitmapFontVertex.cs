using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.Fonts
{
    public struct BitmapFontVertex
    {
        public Vector2 Position;
        public Vector2 TexCoord;
        public int PageIndex;

        public BitmapFontVertex(Vector2 position, Vector2 texCoord, int pageIndex)
        {
            Position = position;
            TexCoord = texCoord;
            PageIndex = pageIndex;
        }
    }
}
