using Vildmark.Audio.ALObjects;
using Vildmark.Resources;

namespace Vildmark.Audio.Loaders;

internal class AudioTrackResourceLoader : IResourceLoader<AudioTrack>
{
    public AudioTrack Load(string name, ResourceLoadContext context)
    {
        return new(context.Load<ALBuffer>(name));
    }
}
