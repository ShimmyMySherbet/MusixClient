using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Core.Helpers
{
    public static class YoutubeHeleprs
    {
        public static string GetVideoID(string URL)
        {
            if (URL.ToLower().Contains("youtube.com"))
            {
                return Pattern1(URL);
            } else if (URL.ToLower().Contains("youtu.be"))
            {
                return Pattern2(URL);
            } else
            {
                return null;
            }
        }
        private static string Pattern1(string URL)
        {
 URL = URL.Replace("?", "&");
            string[] Parts = URL.Split('&');
            foreach(string prt in Parts)
            {
                if (prt.Contains("="))
                {
                    string Key = prt.Split('=')[0];
                    string Value = prt.Remove(0, Key.Length + 1);
                    if (Key.ToLower()=="v")
                    {
                        return WebUtility.UrlDecode(Value);
                    }

                }
            }
            return null;
        }
        private static string Pattern2(string URL)
        {
            string Base = URL.Split('?')[0];
            return Base.Substring(Base.IndexOf("youtu.be", 0, Base.Length, StringComparison.CurrentCultureIgnoreCase) + 8).Trim('/');
        }
    }
}
