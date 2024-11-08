using Vildmark.Audio.ALObjects;
using Vildmark.Audio.Loaders;
using Vildmark.Resources;

namespace Vildmark.Audio;

[ResourceLoader(typeof(AudioTrackResourceLoader))]
public class AudioTrack(ALBuffer buffer)
{
    internal ALBuffer Buffer { get; } = buffer;

    public void Play(AudioPlayer? player = default)
    {
        player ??= AudioPlayer.Default;

        player.Play(this);
    }
}
