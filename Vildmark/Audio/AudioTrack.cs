using Vildmark.Audio.ALObjects;

namespace Vildmark.Audio;

public class AudioTrack
{
    internal ALBuffer Buffer { get; }

    public AudioTrack(ALBuffer buffer)
    {
        Buffer = buffer;
    }

    public void Play(AudioPlayer? player = default)
    {
        player ??= AudioPlayer.Default;

        player.Play(this);
    }
}
