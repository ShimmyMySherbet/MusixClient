using Musix.Core.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System;

namespace Musix.Core.Modules
{
    public static class SpotifyTrackFinder
    {
        public static bool DebugMode = false;
        public static FullTrack FindTrack(SpotifyWebAPI Spotify, ExtrapResult Ext, TimeSpan BaseLength, double MaxDeviation)
        {
            PrintDebug($"searching tracks. Extrap; Name: {Ext.TrackName}, Artist: {Ext?.TrackArtist}. " +
                $"   Source: {Ext.Source}");
            SearchItem Results = Spotify.SearchItems(Ext.GetSearchTerm(), SpotifyAPI.Web.Enums.SearchType.Track, 20);
            FullTrack SelectedResult = null;
            foreach (var Result in Results.Tracks.Items)
            {
                PrintDebug("");
                PrintDebug($"Spotify Track: {Result.Name}");
                PrintDebug($"Spotify Artists: {string.Join(", ", Result.Artists.CastEnumerable(x => x.Name))}");
                if (SelectedResult == null && ContainsMatch(Ext.TrackName, Ext.Source, Result.Name))
                {
                    PrintDebug("[Check] Name Passed");
                    if (!(Math.Abs(Result.DurationMs - BaseLength.TotalMilliseconds) > MaxDeviation))
                    {
                        PrintDebug("[Check] Dur Passed");
                        if (!(string.IsNullOrEmpty(Ext.TrackArtist)) &&  ContainsMatch(Ext.TrackArtist, Ext.Source, Result.Artists[0].Name))
                            {
                            PrintDebug("[Check] Len Passed");
                            SelectedResult = Result;
                        }
                        else
                        {
                            PrintDebug("[Check] len Failed");
                        }
                    }
                    else
                    {
                        PrintDebug("[Check] dur Failed");
                    }
                } else
                {
                    PrintDebug("[Check] name Failed");
                }
            }
            PrintDebug("Returned Tracks");
            return SelectedResult;
        }

        private static void PrintDebug(string msg) {
            if (DebugMode) Console.WriteLine(msg);
        }
        private static bool ContainsMatch(string Selected, string Source, string Match)
        {
            string sel = Selected.ToLower();
            if (!string.IsNullOrEmpty(Source)) sel = Source.ToLower();
            foreach (string ent in Match.Split(' '))
            {
                if (!sel.Contains(ent.ToLower()) && !Selected.ToLower().Contains(ent.ToLower())) return false;
            }
            return true;
        }
    }
}