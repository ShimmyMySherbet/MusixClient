using System;
using Musix.Core.API;
using Musix.Core.Models;

namespace Musix.Core.Components
{
    public class DirectTrackExtrapolator : IDetailsExtrapolator
    {
        public ExtrapResult ExtrapolateDetails(string Term)
        {
            int FPos = Term.IndexOf("ft.", 0, Term.Length, StringComparison.InvariantCultureIgnoreCase);

            if (FPos != -1)
            {
                Term = Term.Substring(0, FPos).Trim(' ');
            }

            bool InB = false;
            bool InSQB = false;
            string Ent = "";
            string Remix = "";
            string BracketContent = "";
            string SQBracketContent = "";
            foreach (char cha in Term)
            {
                if (!InB & !InSQB)
                {
                    if (cha == '(')
                    {
                        InB = true;
                    }
                    else if (cha == '[')
                    {
                        InSQB = true;
                    }
                    else
                    {
                        Ent += cha;
                    }
                }
                else
                {
                    if (cha == ')')
                    {
                        if (InSQB) SQBracketContent += cha;
                        InB = false;
                        if (BracketContent.ToLower().Contains("remix"))
                        {
                            Remix = BracketContent;
                        }
                        BracketContent = "";
                    }
                    else if (cha == ']')
                    {
                        if (InB) BracketContent += cha;
                        InSQB = false;
                        if (SQBracketContent.ToLower().Contains("remix"))
                        {
                            Remix = SQBracketContent;
                        }
                        SQBracketContent = "";
                    }
                    else
                    {
                        if (InB) BracketContent += cha;
                        if (InSQB) SQBracketContent += cha;
                    }
                }
            }
            Term = Ent;
            ExtrapResult Result = new ExtrapResult() { Source = Term };
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
            if (Remix.Length != 0)
            {
                Result.TrackName += " " + Remix;
            }
            return Result;
        }
    }
}