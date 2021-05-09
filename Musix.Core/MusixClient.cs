using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Musix.Core.Abstractions;
using Musix.Core.Models;

namespace Musix.Core
{
    public class MusixClient
    {
        public IContextFactory ContextFactory;
        public IMetaProvider MetaManager;
        public IAudioProvider AudioSource;
        public ITranscoderIndex TranscoderIndex;
        public IInputMetaProvider InputMetaProvider;
        public ICacheProvider CacheProvider;
        public IMetaDataWriter MetaWriter;
        public IAudioEffectStack AudioEffects;

        /// <summary>
        /// Only needed when the cache provider doesn't support path hosting, and the selected transcoder requires a path.
        /// </summary>
        public IFileHoster FileHoster;

        public bool CanProvideFileHosting
        {
            get
            {
                return FileHoster != null;
            }
        }

        public async Task Download(string input)
        {
            DownloadContext context = ContextFactory.CreateContext(input);
            await Download(context);
        }

        public async Task Downlaod(string input, Dictionary<string, object> meta)
        {
            DownloadContext context = ContextFactory.CreateContext(input, meta);
            await Download(context);
        }

        private ETranscoderPreferance GetTranscoderPreferance(DownloadContext context)
        {
            ETranscoderPreferance preferance;
            if (CanProvideFileHosting)
            {
                preferance = ETranscoderPreferance.NoPathedOnly;
            }
            else if (context.TranscoderPreferance == ETranscoderPreferance.None)
            {
                preferance = ETranscoderPreferance.PreferNoPathed;
            }
            else
            {
                preferance = context.TranscoderPreferance;
            }
            return preferance;
        }

        public async Task<bool> Download(DownloadContext download)
        {
            if (download.InitialMeta != null)
            {
                InputMetaProvider.DerriveMeta(download.InitialMeta, download);
            }

            ETranscoderPreferance preferance = GetTranscoderPreferance(download);

            using (download.CacheShard = CacheProvider.CreateShard())
            using (ICachedAsset audioAsset = download.CacheShard.CreateAsset("Audio"))
            using (ICachedAsset transcodedAudioAsset = download.CacheShard.CreateAsset("TranscodedAudio"))
            using (ICachedAsset audioEffects = download.CacheShard.CreateAsset("audioEffects"))
            using (TaskList tasks = new TaskList())
            {
                IAudioSource audioSource = await AudioSource.GetAudioSource(download, preferance);

                if (AudioSource == null)
                {
                    return false;
                }

                tasks.Add(audioSource.DownloadAudio(download, audioAsset.Stream));
                tasks.Add(MetaWriter.PrepareAssets(download));

                await tasks.WaitAll();

                AudioDownloadResult res = tasks.GetResult<AudioDownloadResult>();

                if (!res.Success) return false;

                ITranscoder transcoder = TranscoderIndex.FindTranscoder(audioSource.OutputFormat, download.OutputFormat, preferance);

                await transcoder.Transcode(audioAsset.Stream, transcodedAudioAsset.Stream, download);

                Stream metaWrite = transcodedAudioAsset.Stream;

                if (AudioEffects.Count > 0)
                {
                    if (await AudioEffects.ApplyEffects(transcodedAudioAsset.Stream, audioEffects.Stream, download))
                    {
                        metaWrite = audioEffects.Stream;
                    }
                }

                await MetaWriter.WriteMeta(metaWrite, download);
            }

            return true;
        }
    }
}