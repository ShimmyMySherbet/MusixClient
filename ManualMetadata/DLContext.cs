using Musix.Core.Components.AudioModifiers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos;

namespace ManualMetadata
{
    public class DLContext
    {
        public VideoId YTID;
        public string TrackName;
        public string AlbumName;
        public string ArtistName;
        public Stream Artwork;
        public AudioTrimmer Trimmer;
        public Stream CoverStream;
    }
}
