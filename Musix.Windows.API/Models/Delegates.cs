using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Windows.API.Models
{
    public class Delegates
    {
        public delegate void UpdateIconArgs(Image image);
        public delegate void OnManualResolveCancelled();
        public delegate void OnManualResolveFinished(string SpotifyTrackID, string YoutubeTrackID);
        public delegate void OnPopupCloseRequestedArgs(object sender, EventArgs e);
    }
}
