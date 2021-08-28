using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Providers.YouTube.Exceptions
{
    public sealed class NoAudioStreamsException : Exception
    {
        public override string Message => "No audio streams foud or readable.";
    }
}
