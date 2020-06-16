using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Core.Models
{
    public class ExtrapResult
    {
        public string Source;
        public string TrackName;
        public string TrackArtist;

        public string GetSearchTerm()
        {
            if (!string.IsNullOrEmpty(TrackArtist))
            {
                return $"{TrackArtist} - {TrackName}";
            } else
            {
                return TrackName;
            }
        }
    }
}
