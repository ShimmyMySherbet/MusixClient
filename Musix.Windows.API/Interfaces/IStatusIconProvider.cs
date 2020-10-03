using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;

namespace Musix.Windows.API.Interfaces
{
    public interface IStatusIconProvider
    {
        event Delegates.UpdateIconArgs UpdateIcon;
        void Init();
        void StyleChanged(EStyle style);
        bool DisposeOnUpdate { get; }
    }
}
