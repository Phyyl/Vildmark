using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.GLObjects;

public abstract class GLBuffer : GLObject
{
    public BufferTarget BufferTarget { get; }

    public BufferUsageHint BufferUsageHint { get; }

    public int Count { get; protected set; }

    public abstract int ElementSize { get; }

    protected GLBuffer(BufferTarget bufferTarget, BufferUsageHint bufferUsageHint)
        : base(GL.GenBuffer())
    {
        BufferTarget = bufferTarget;
        BufferUsageHint = bufferUsageHint;
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget, this);
    }

    public void Unbind()
    {
        Unbind(BufferTarget);
    }

    public static void Unbind(BufferTarget bufferTarget)
    {
        GL.BindBuffer(bufferTarget, 0);
    }

    protected override void DisposeOpenGL()
    {
        GL.DeleteBuffer(this);
    }
}

public unsafe class GLBuffer<T> : GLBuffer where T : unmanaged
{
    public override int ElementSize => sizeof(T);

    public GLBuffer(int size = default, BufferTarget bufferTarget = BufferTarget.ArrayBuffer, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw)
        : this(new Span<T>(new T[size]), bufferTarget, bufferUsageHint)
    {
    }

    public GLBuffer(Span<T> data, BufferTarget bufferTarget = BufferTarget.ArrayBuffer, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw)
        : base(bufferTarget, bufferUsageHint)
    {
        SetData(data);
    }

    public unsafe void SetData(Span<T> data)
    {
        Bind();

        if (data.IsEmpty)
        {
            GL.BufferData(BufferTarget, 0, IntPtr.Zero, BufferUsageHint);
        }
        else
        {
            GL.BufferData(BufferTarget, data.Length * sizeof(T), ref MemoryMarshal.GetReference(data), BufferUsageHint);
        }

        Count = data.Length;
    }

    public void Map(out Span<T> data)
    {
        Map(BufferAccess.ReadWrite, out data);
    }

    public void MapReadOnly(out ReadOnlySpan<T> data)
    {
        Map(BufferAccess.ReadOnly, out Span<T> mapped);

        data = mapped;
    }

    public void MapWriteOnly(out Span<T> data)
    {
        Map(BufferAccess.WriteOnly, out data);
    }

    public void Unmap()
    {
        Unmap(BufferTarget);
    }

    public static void Unmap(BufferTarget bufferTarget)
    {
        GL.UnmapBuffer(bufferTarget);
    }

    private unsafe void Map(BufferAccess bufferAccess, out Span<T> data)
    {
        Bind();

        GL.GetBufferParameter(BufferTarget, BufferParameterName.BufferSize, out int size);
        IntPtr ptr = GL.MapBuffer(BufferTarget, bufferAccess);

        data = new Span<T>(ptr.ToPointer(), size / sizeof(T));
    }
}
