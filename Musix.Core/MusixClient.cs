using Musix.Core.Abstractions;

namespace Musix.Core
{
    public class MusixClient
    {
        public IContextBuilder ContextBuilder;
        public IMetaProvider MetaManager;
        public IAudioProvider AudioSource;
        public ITranscoderIndex TranscoderIndex;
    }
}