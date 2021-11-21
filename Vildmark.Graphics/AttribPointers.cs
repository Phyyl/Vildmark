using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics
{
    public record AttribPointer(int Index, int Size, VertexAttribPointerType Type, int Stride, int Offset);

    public interface IAttribPointers
    {
        public IEnumerable<AttribPointer> GetAttribPointers();
    }
}
