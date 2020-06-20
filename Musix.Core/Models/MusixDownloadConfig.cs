using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Core.Models
{
    public class MusixDownloadConfig
    {
        public bool TrimAudio;
        public TimeSpan? TrimStartTime;
        public TimeSpan? TrimEndTime;
    }
}
