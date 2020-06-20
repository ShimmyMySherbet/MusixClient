using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Core.Helpers
{
    public static class DownloadHelpers
    {
        public static bool IsNumeric(string inp)
        {
            string Allowed = "0123456789";
            foreach(char cha in inp)
            {
                if (!Allowed.Contains(cha)) return false;
            }
            return true;

        }
    }
}
