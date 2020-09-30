using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Windows.API.Themes;

namespace Musix.Windows.API.Interfaces
{
    public interface IStyleableControl
    {
        void SendStyle(EStyle Style);
    }
}
