using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.Fonts
{
    public struct BitmapFontVertex : IVertex
    {
        private static readonly int size = Marshal.SizeOf<BitmapFontVertex>();

        public int Size => size;
    }
}
