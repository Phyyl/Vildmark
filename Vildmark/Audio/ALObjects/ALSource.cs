using OpenTK.Audio.OpenAL;

namespace Vildmark.Audio.ALObjects;

public class ALSource : ALObject
{
    public ALSourceState State => OpenALContext.GetSourceState(this);

    public ALSource()
        : base(OpenALContext.GenSource())
    {

    }

    public void Play()
    {
        if (State == ALSourceState.Playing)
        {
            return;
        }

        AL.SourcePlay(this);
    }

    public void Pause() => AL.SourcePause(this);
    public void Rewind() => AL.SourceRewind(this);
    public void Stop() => AL.SourceStop(this);
    public void Queue(ALBuffer buffer) => AL.SourceQueueBuffer(this, buffer);
    public int Dequeue() => AL.SourceUnqueueBuffer(this);

    protected override void DisposeOpenAL()
    {
        AL.DeleteSource(this);
    }
}
