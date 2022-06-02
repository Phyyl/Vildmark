using Vildmark.Audio.ALObjects;
using Vildmark.Audio.Loaders;
using Vildmark.Resources;

namespace Vildmark.Audio;

public class AudioTrack : IResource<AudioTrack>
{
    public static IResourceLoader<AudioTrack> Loader { get; } = new AudioTrackResourceLoader();

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
