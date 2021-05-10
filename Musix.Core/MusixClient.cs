using System.Collections.Generic;
using System.Threading.Tasks;
using Musix.Core.Abstractions;
using Musix.Core.Models;

namespace Musix.Core
{
    public class MusixClient
    {
        private IContextFactory m_ContextFactory;
        private IAudioProvider m_AudioSource;
        private IAudioEffectStack m_AudioEffects;
        private ITranscoderIndex m_TranscoderIndex;
        private IInputMetaProvider m_InputMetaProvider;
        private ICacheProvider m_CacheProvider;
        private IMetaProvider m_MetaProvider;
        private IMetaDataWriter m_MetaWriter;

        public MusixClient(MusixConfig config)
        {
            InitWithConfig(config);
        }

        public MusixClient()
        {
            InitWithConfig(new MusixConfig());
        }

        private void InitWithConfig(MusixConfig config)
        {
            m_ContextFactory = config.ContextFactory;
            m_MetaProvider = config.MetaManager;
            m_AudioSource = config.AudioSource;
            m_TranscoderIndex = config.TranscoderIndex;
            m_InputMetaProvider = config.InputMetaProvider;
            m_CacheProvider = config.CacheProvider;
            m_MetaWriter = config.MetaWriter;
            m_AudioEffects = config.AudioEffects;
        }

        /// <summary>
        /// Only needed when the cache provider doesn't support path hosting, and the selected transcoder requires a path.
        /// </summary>
        private IFileHoster m_FileHoster;

        public bool CanProvideFileHosting
        {
            get
            {
                return m_FileHoster != null;
            }
        }

        public async Task Download(string input)
        {
            DownloadContext context = m_ContextFactory.CreateContext(input);
            await Download(context);
        }

        public async Task Downlaod(string input, Dictionary<string, object> meta)
        {
            DownloadContext context = m_ContextFactory.CreateContext(input, meta);
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
                m_InputMetaProvider.DerriveMeta(download.InitialMeta, download);
            }

            ETranscoderPreferance preferance = GetTranscoderPreferance(download);

            using (download.CacheShard = m_CacheProvider.CreateShard())
            using (ICachedAsset audioAsset = download.CacheShard.CreateAsset("Audio"))
            using (ICachedAsset transcodedAudioAsset = download.CacheShard.CreateAsset("TranscodedAudio"))
            using (ICachedAsset audioEffects = download.CacheShard.CreateAsset("audioEffects"))
            using (TaskList tasks = new TaskList())
            {
                IAudioSource audioSource = await m_AudioSource.GetAudioSource(download, preferance);

                if (m_AudioSource == null)
                {
                    return false;
                }

                tasks.Add(audioSource.DownloadAudio(download, audioAsset.Stream));
                tasks.Add(m_MetaProvider.PrepareAssets(download));

                await tasks.WaitAll();

                AudioDownloadResult res = tasks.GetResult<AudioDownloadResult>();

                if (!res.Success) return false;

                ITranscoder transcoder = m_TranscoderIndex.FindTranscoder(audioSource.OutputFormat, download.OutputFormat, preferance);

                audioAsset.ResetPosition();
                await transcoder.Transcode(audioAsset.Stream, transcodedAudioAsset.Stream, download);

                ICachedAsset metaWrite = transcodedAudioAsset;

                if (m_AudioEffects.Count > 0)
                {
                    transcodedAudioAsset.ResetPosition();
                    if (await m_AudioEffects.ApplyEffects(transcodedAudioAsset.Stream, audioEffects.Stream, download))
                    {
                        metaWrite = audioEffects;
                    }
                }

                metaWrite.ResetPosition();
                await m_MetaWriter.WriteMeta(metaWrite.Stream, download);
                metaWrite.Stream.CopyTo(download.OutputStream);
            }

            return true;
        }
    }
}