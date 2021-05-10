using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public struct AudioSourceAvailability
    {
        public bool Available { get; private set; }
        public float Bitrate { get; private set; }
        public IAudioSource Source { get; private set; }

        public AudioSourceAvailability(bool available, IAudioSource source)
        {
            Available = available;
            Source = source;
            Bitrate = -1;
        }

        public AudioSourceAvailability(bool available, IAudioSource source, float bitrate)
        {
            Available = available;
            Source = source;
            Bitrate = bitrate;
        }
    }
}