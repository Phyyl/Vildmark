using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Audio.ALObjects;

namespace Vildmark.Audio
{
    public class AudioTrack
    {
        internal ALBuffer Buffer { get; }

        internal AudioTrack(ALBuffer buffer)
        {
            Buffer = buffer;
        }

        public void Play(AudioPlayer? player = default)
        {
            player ??= AudioPlayer.Default;

            player.Play(this);
        }
    }
}
