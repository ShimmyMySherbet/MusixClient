using Musix.Core.Abstractions;
using Musix.Core.Models;
using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Musix.Providers.YouTube
{
    public class YoutubeAudioFactory : IAudioSourceFactory
    {
        public async Task<AudioSourceAvailability> CheckValid(DownloadContext context)
        {
            try
            {
                var ID = VideoId.Parse(context.MetaIndex.GetString("YT-ID"));
                YoutubeClient client = new YoutubeClient();

                var manifest = await client.Videos.Streams.GetManifestAsync(ID);

                if (manifest != null)
                {
                    context.MetaIndex.Set("YT-MANIFEST", manifest);
                    return new AudioSourceAvailability(true, this, 128f);
                }
                else
                {
                    return new AudioSourceAvailability(false, this);
                }
            }
            catch (Exception ex)
            {
                return new AudioSourceAvailability(ex, this);
            }
        }

        public async Task<IAudioSource> CreateAudioSource(DownloadContext context)
        {
            StreamManifest manifest = null;

            if (context.MetaIndex.ContainsKey("YT-MANIFEST"))
            {
                manifest = context.MetaIndex.Get<StreamManifest>("YT-MANIFEST");
            }
            else if (context.MetaIndex.ContainsKey("YT-ID"))
            {
                manifest = await GetManifest(context.MetaIndex.GetString("YT.ID"));
            }
            else
            {
                return null;
            }
            return new YoutubeAudioProvider(manifest);
        }

        private async Task<StreamManifest> GetManifest(string YTID)
        {
            var ID = VideoId.Parse(YTID);
            YoutubeClient client = new YoutubeClient();

            return await client.Videos.Streams.GetManifestAsync(ID);
        }
    }
}