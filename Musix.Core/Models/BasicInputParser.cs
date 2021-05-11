using System;
using Musix.Core.Abstractions;
using Musix.Core.Attributes;

namespace Musix.Core.Models
{
    [Weight(-100)]
    public class BasicInputParser : IInputMetaParser
    {
        public bool DerriveMeta(string input, DownloadContext context)
        {
            if (context.MetaIndex.ContainsKey("MetaTrack") || context.MetaIndex.ContainsKey("MetaArtist"))
            {
                return false;
            }

            int FPos = input.IndexOf("ft.", 0, input.Length, StringComparison.InvariantCultureIgnoreCase);
            if (FPos != -1)
            {
                input = input.Substring(0, FPos).Trim(' ');
            }

            bool InB = false;
            bool InSQB = false;
            string Ent = "";
            string Remix = "";
            string BracketContent = "";
            string SQBracketContent = "";
            foreach (char cha in input)
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
            input = Ent;
            input.Replace('|', '-');
            string trackName;
            if (input.Contains("-"))
            {
                string artist = input.Split('-')[0];
                string Track = input.Remove(0, artist.Length + 1);
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
                    context.MetaIndex.Set("MetaArtist", artist.Trim(' '));
                }
                trackName = Track.Trim(' ');
            }
            else
            {
                trackName = input.Trim(' ');
            }
            if (Remix.Length != 0)
            {
                trackName += " " + Remix;
            }
            context.MetaIndex.Set("MetaTrack", trackName);
            return true;
        }
    }
}