using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class MusixConfig
    {
        public IContextFactory ContextFactory;
        public IMetaProvider MetaProvider;
        public IAudioProvider AudioSource;
        public ITranscoderIndex TranscoderIndex;
        public IInputMetaProvider InputMetaProvider;
        public ICacheProvider CacheProvider;
        public IMetaDataWriter MetaWriter;
        public IAudioEffectStack AudioEffects;
        public IFileHoster FileHoster;

        public MusixConfig(IMetaDataWriter metaWriter, IAudioEffectStack audioEffects = null, IFileHoster fileHoster = null)
        {
            ContextFactory = new DefaultContextFactory();
            MetaProvider = new MetaProvider();
            AudioSource = new AudioProvider();
            TranscoderIndex = new TranscoderIndex();
            InputMetaProvider = new InputMetaProvider();
            CacheProvider = new FileCacheProvider();
            MetaWriter = metaWriter;
            AudioEffects = audioEffects;
            FileHoster = fileHoster;
        }
    }
}