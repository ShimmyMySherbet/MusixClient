using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;

namespace Musix.Models
{
    public class MusixUIManager : IUIManager
    {


        public void AddMenuPage(Control control, bool forceNew)
        {

        }

        public void RemoveMenuPage(Control control)
        {
            throw new NotImplementedException();
        }

        public void RemovemenuPage<T>() where T : Control
        {
            throw new NotImplementedException();
        }

        public void SendSelectMenuTab<T>() where T : IMusixMenuItem
        {
            throw new NotImplementedException();
        }

        public void ShowMenuPage(Control control)
        {
            throw new NotImplementedException();
        }

        public void ShowMenuPage<T>() where T : Control
        {
            throw new NotImplementedException();
        }
    }
}
