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
        public MusixConfig()
        {
            ContextFactory = new DefaultContextFactory();
            MetaProvider = new MetaProvider();
            AudioSource = new AudioProvider();
            TranscoderIndex = null;
            InputMetaProvider = null;
            CacheProvider = new FileCacheProvider();
            MetaWriter = null;
            AudioEffects = null;
            FileHoster = null;
        }
    }
}