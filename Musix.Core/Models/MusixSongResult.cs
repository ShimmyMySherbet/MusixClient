using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos;

namespace Musix.Core.Models
{
    public class MusixSongResult
    {
        public Video YoutubeVideo;
        public FullTrack SpotifyTrack;
        public GeniusResult Lyrics;
        public ExtrapResult Extrap;
        public bool HasVideo { get { return YoutubeVideo != null; } }
        public bool HasTrack { get { return SpotifyTrack != null; } }
        public bool HasLyrics;

    }
}
