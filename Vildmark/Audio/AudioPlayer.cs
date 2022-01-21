using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Audio.ALObjects;

namespace Vildmark.Audio
{
    public class AudioPlayer
    {
        private readonly ALSource[] sources;
        private int nextSource;

        public AudioPlayer(int channels = 64)
        {
            sources = Enumerable.Range(0, channels).Select(_ => new ALSource()).ToArray();
        }

        public void Play(ALBuffer buffer)
        {
            Play(buffer, nextSource);
            nextSource = (nextSource + 1) % sources.Length;
        }

        public void Play(ALBuffer buffer, int channel)
        {
            ALSource source = sources[channel];

            source.Queue(buffer);
            source.Play();
        }
    }
}
