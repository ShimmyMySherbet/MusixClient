using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;
using YoutubeExplode.Videos;

namespace Musix.Core.Models.Interfaces
{
    public interface IMusixCollector
    {
        MusixSongResult Collect(Video video);
        MusixSongResult Collect(string VideoURL);
        MusixSongResult Collect(FullTrack track);

        Task<MusixSongResult> CollectAsync(string VideoURL);
        Task<MusixSongResult> CollectAsync(Video video);
        Task<MusixSongResult> CollectAsync(FullTrack track);


    }
}
