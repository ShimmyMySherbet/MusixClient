using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Windows.API.Models;

namespace Musix.Windows.API.Interfaces
{
    public interface IPopupItem
    {
        void OnPopupOpen();
        bool OnBeforePopupClose();
        void OnPopupClose();
    }
}
