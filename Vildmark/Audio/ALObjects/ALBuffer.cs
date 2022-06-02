using OpenTK.Audio.OpenAL;
using Vildmark.Resources;

namespace Vildmark.Audio.ALObjects;

public class ALBuffer : ALObject, IResource<ALBuffer>
{
    public static IResourceLoader<ALBuffer> Loader { get; } = new ALBufferResourceLoader();

    public ALBuffer()
        : base(OpenALContext.GenBuffer())
    {
    }

    private ALBuffer(int id)
        : base(id)
    {
    }

    public void SetData<T>(Span<T> data, int channels, int bitsPerSample, int sampleRate = 44100) where T : unmanaged
    {
        SetData(data, GetFormat(channels, bitsPerSample), sampleRate);
    }

    public void SetData<T>(Span<T> data, ALFormat format = ALFormat.Stereo16, int sampleRate = 44100) where T : unmanaged
    {
        AL.BufferData<T>(this, format, data, sampleRate);
    }

    protected override void DisposeOpenAL()
    {
        AL.DeleteBuffer(this);
    }

    private static ALFormat GetFormat(int channels, int bitsPerSample) => (channels, bitsPerSample) switch
    {
        (1, 8) => ALFormat.Mono8,
        (1, 16) => ALFormat.Mono16,
        (2, 8) => ALFormat.Stereo8,
        (2, 16) => ALFormat.Stereo16,
        _ => throw new FormatException($"Invalid format with {channels} channels and {bitsPerSample} bits per sample")
    };

    public static implicit operator ALBuffer(int id) => new(id);
}
