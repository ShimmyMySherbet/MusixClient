using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Abstractions;
using Musix.Core.Models;

namespace Musix.Providers.YouTube
{
    public class YoutubeAudioProvider : IAudioSource
    {
        public string Name => "YouTube";

        public string OutputFormat => "Webm";

        public Task<AudioDownloadResult> DownloadAudio(DownloadContext context, Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task<AudioSourceAvailability> VerifyAvailable(DownloadContext context)
        {
            throw new NotImplementedException();
        }
    }
}
