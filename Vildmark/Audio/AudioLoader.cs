using System.Reflection;
using Vildmark.Audio.ALObjects;
using Vildmark.Resources;

namespace Vildmark.Audio
{
    [Register(typeof(IResourceLoader<ALBuffer>))]
    [Register(typeof(IResourceLoader<AudioTrack>))]
    public class AudioLoader : IResourceLoader<ALBuffer>, IResourceLoader<AudioTrack>
    {
        public ALBuffer? Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            byte[] data = LoadWav(stream, out int channels, out int bitsPerSample, out int sampleRate);
            ALBuffer buffer = new();

            buffer.SetData(data.AsSpan(), channels, bitsPerSample, sampleRate);

            return buffer;
        }

        private static byte[] LoadWav(Stream stream, out int channels, out int bitsPerSample, out int sampleRate)
        {
            using BinaryReader reader = new(stream ?? throw new ArgumentNullException(nameof(stream)));

            string ReadString(int count = 4) => new(reader.ReadChars(count));

            string signature = ReadString();
            int chunkSize = reader.ReadInt32();
            string format = ReadString();
            string formatSignature = ReadString();

            if (signature != "RIFF" || format != "WAVE" || formatSignature != "fmt ")
            {
                throw new InvalidDataException("Data does not represent WAV audio");
            }

            int formatChunkSize = reader.ReadInt32();
            int audioFormat = reader.ReadInt16();
            channels = reader.ReadInt16();
            sampleRate = reader.ReadInt32();
            int byteRate = reader.ReadInt32();
            int blockAlign = reader.ReadInt16();
            bitsPerSample = reader.ReadInt16();

            string dataSignature = ReadString();

            if (dataSignature != "data")
            {
                throw new InvalidDataException("Invalid WAV audio data");
            }

            int dataChunkSize = reader.ReadInt32();

            return reader.ReadBytes((int)reader.BaseStream.Length);
        }

        AudioTrack? IResourceLoader<AudioTrack>.Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            if (Load(stream, assembly, resourceName) is not ALBuffer buffer)
            {
                return default;
            }

            return new AudioTrack(buffer);
        }
    }
}
