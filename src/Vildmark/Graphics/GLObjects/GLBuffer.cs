using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.GLObjects;

internal abstract class GLBuffer : GLObject
{
    public BufferTarget BufferTarget { get; }

    public BufferUsage BufferUsage { get; }

    public int Count { get; protected set; }

    public abstract int ElementSize { get; }

    protected GLBuffer(BufferTarget bufferTarget, BufferUsage bufferUsage)
        : base(GL.GenBuffer())
    {
        BufferTarget = bufferTarget;
        BufferUsage = bufferUsage;
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

    protected override void DisposeOpenGL(ref int id)
    {
        GL.DeleteBuffer(ref id);
    }
}

internal unsafe class GLBuffer<T> : GLBuffer where T : unmanaged
{
    public override int ElementSize => sizeof(T);

    public GLBuffer(int size = default, BufferTarget bufferTarget = BufferTarget.ArrayBuffer, BufferUsage bufferUsage = BufferUsage.StaticDraw)
        : this(new Span<T>(new T[size]), bufferTarget, bufferUsage)
    {
    }

    public GLBuffer(Span<T> data, BufferTarget bufferTarget = BufferTarget.ArrayBuffer, BufferUsage bufferUsage = BufferUsage.StaticDraw)
        : base(bufferTarget, bufferUsage)
    {
        SetData(data);
    }

    public unsafe void SetData(Span<T> data)
    {
        Bind();

        if (data.IsEmpty)
        {
            GL.BufferData(BufferTarget, 0, IntPtr.Zero, BufferUsage);
        }
        else
        {
            GL.BufferData(BufferTarget, data.Length * sizeof(T), ref MemoryMarshal.GetReference(data), BufferUsage);
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

    private unsafe IDisposable Map(BufferAccess bufferAccess, out Span<T> data)
    {
        Bind();

        GL.GetBufferParameteri(BufferTarget, BufferPName.BufferSize, out int size);
        void* ptr = GL.MapBuffer(BufferTarget, bufferAccess);

        data = new Span<T>(ptr, size / sizeof(T));

        return new DisposableMap(BufferTarget);
    }

    private class DisposableMap(BufferTarget bufferTarget) : IDisposable
    {
        public void Dispose()
        {
            Unmap(bufferTarget);
        }
    }
}
