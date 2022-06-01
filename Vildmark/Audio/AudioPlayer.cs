using Vildmark.Audio.ALObjects;

namespace Vildmark.Audio;

public class AudioPlayer
{
    private static AudioPlayer? @default;

    public static AudioPlayer Default => @default ??= new(2);

    private readonly ALSource[] sources;
    private int nextSource;

    public AudioPlayer(int channels = 256)
    {
        sources = Enumerable.Range(0, channels).Select(_ => new ALSource()).ToArray();
    }

    public void Play(AudioTrack track)
    {
        Play(track, nextSource);
        nextSource = (nextSource + 1) % sources.Length;
    }

    public void Play(AudioTrack track, int channel)
    {
        ALSource source = sources[channel];

        source.Stop();
        source.Dequeue();
        source.Queue(track.Buffer);
        source.Play();
    }
}
