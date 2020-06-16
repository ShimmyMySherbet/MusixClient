using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.Swan;

namespace Musix.Core.Helpers
{
    public static class FileHelpers
    {
        public static char[] ForbiddenCharacters = { '*', '"', '/', '\\', '[', ']', ':', ';', '|', ',' };
        public static string ScrubFileName(string Filename)
        {
            foreach(char cha in ForbiddenCharacters)
            {
                Filename = Filename.Replace(cha.ToString(), "");
            }
            return Filename;
        }
    }
}
