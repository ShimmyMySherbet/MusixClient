using System.Windows.Forms;
using Musix.Windows.API.Models;

namespace Musix.Windows.API.Interfaces
{
    public interface IUIManager
    {
        void AddMenuPage(Control control, bool forceNew);

        void RemoveMenuPage(Control control);

        void RemovemenuPage<T>() where T : Control;

        void ShowMenuPage(Control control);

        void ShowMenuPage<T>() where T : Control;

        void SendSelectMenuTab<T>() where T : IMusixMenuItem;
    }
}