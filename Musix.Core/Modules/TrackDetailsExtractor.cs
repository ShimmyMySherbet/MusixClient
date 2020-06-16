using Musix.Core.Models;

namespace Musix.Core.Modules
{
    public static class TrackDetailsExtractor
    {
        public static ExtrapResult ExtrapolateDetails(string Term)
        {
            bool InB = false;
            string Ent = "";
            string Remix = "";
            string BracketContent = "";
            foreach (char cha in Term)
            {
                if (!InB)
                {
                    if (cha == '(')
                    {
                        InB = true;
                    } else
                    {
                        Ent += cha;
                    }
                }
                else
                {
                    if (cha == ')')
                    {
                        InB = false;
                        if (BracketContent.ToLower().Contains("remix"))
                        {
                            Remix = BracketContent;
                        }
                        BracketContent = "";
                    } else
                    {
                        BracketContent += cha;
                    }
                }
            }
            Term = Ent;
            ExtrapResult Result = new ExtrapResult() { Source = Term};
            Term.Replace('|', '-');
            if (Term.Contains("-"))
            {
                string artist = Term.Split('-')[0];
                string Track = Term.Remove(0, artist.Length + 1);
                if (Track.Contains("-"))
                {
                    Track = Track.Split('-')[0];
                }
                if (artist.Contains("&"))
                {
                    artist = artist.Split('&')[0];
                }
                if (artist.Contains("-"))
                {
                    artist = artist.Split('-')[0];
                }
                if (!string.IsNullOrEmpty(artist))
                {
                    Result.TrackArtist = artist;
                }
                Result.TrackName = Track;
            }
            else
            {
                Result.TrackName = Term;
                Result.TrackArtist = "";
            }
            Result.TrackArtist = Result.TrackArtist.Trim(' ');
            Result.TrackName = Result.TrackName.Trim(' ');
            if (Remix != "")
            {
                Result.TrackName += " " + Remix;
            }
            return Result;
        }
    }
}