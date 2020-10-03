using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;

namespace Musix.Controls
{
    public partial class MusixDynamicSidebar : UserControl, IStyleableControl
    {
        public List<IMusixMenuItem> Items = new List<IMusixMenuItem>();

        public delegate void SelectionChangedArgs(IMusixMenuItem SelectedItem);

        public event SelectionChangedArgs OnSelectionChanged;

        public MusixDynamicSidebarItem SelectedItem;

        public EStyle Style = EStyle.Blue;

        private TaskFactory UITaskfactory;
        private TaskScheduler UItaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        public bool IsListeningToCursor = true;

        public MusixDynamicSidebar()
        {
            InitializeComponent();
            UITaskfactory = new TaskFactory(UItaskScheduler);
            new Thread(ListenToCursor).Start();
        }

        private void ListenToCursor()
        {
            Point L = Cursor.Position;
            while (!IsDisposed)
            {
                while (!IsListeningToCursor)
                {
                    Thread.Sleep(100);
                }
                Point N = Cursor.Position;
                if (N != L)
                {
                    L = N;
                    OnCursorMoved();
                }
                Thread.Sleep(50);
            }
        }

        private void OnCursorMoved()
        {
            UITaskfactory.StartNew(() => UpdateHovers());
        }

        public void AddItem(IMusixMenuItem item)
        {
            Items.Add(item);
            MusixDynamicSidebarItem sidebarItem = new MusixDynamicSidebarItem(item);
            sidebarItem.SendStyle(Style);
            sidebarItem.OnSelect += SidebarItem_OnSelect;
            flowElements.Controls.Add(sidebarItem);
        }

        public void UpdateHovers()
        {
            Point cpos = PointToClient(Cursor.Position);
            foreach (MusixDynamicSidebarItem items in flowElements.Controls.OfType<MusixDynamicSidebarItem>())
            {
                CheckHover(items, cpos);
            }
        }

        public void RemoveItem(IMusixMenuItem item)
        {
            MusixDynamicSidebarItem UIElement = GetUIElement(item);
            if (UIElement != null)
            {
                UIElement.OnSelect -= SidebarItem_OnSelect;
                Items.Remove(item);
                flowElements.Controls.Remove(UIElement);
                return;
            }
        }

        public void RemoveItem(MusixDynamicSidebarItem item)
        {
            item.OnSelect -= SidebarItem_OnSelect;
            Items.Remove(item.MenuItem);
            flowElements.Controls.Remove(item);
        }

        public MusixDynamicSidebarItem GetUIElement(IMusixMenuItem item)
        {
            foreach (MusixDynamicSidebarItem sidebarItem in flowElements.Controls.OfType<MusixDynamicSidebarItem>())
            {
                if (sidebarItem.MenuItem == item)
                {
                    return sidebarItem;
                }
            }
            return null;
        }

        public void SelectItemAtIndex(int index)
        {
            IMusixMenuItem item = Items[index];
            System.Console.WriteLine($"Index {index} > {item.Name}");
            MusixDynamicSidebarItem menuItem = GetUIElement(item);
            System.Console.WriteLine($"Resolved entry index: {flowElements.Controls.IndexOf(menuItem)}");
            if (menuItem != null)
            {
                System.Console.WriteLine("setselected");
                menuItem.IsSelected = true;
            }
        }

        public void SelectItem(IMusixMenuItem item)
        {
            MusixDynamicSidebarItem it = GetUIElement(item);
            if (it != null)
            {
                it.IsSelected = true;
            }
        }

        public void DisposeItem(IMusixMenuItem item)
        {
            MusixDynamicSidebarItem UIElement = GetUIElement(item);
            if (UIElement != null)
            {
                UIElement.OnSelect -= SidebarItem_OnSelect;
                Items.Remove(item);
                UIElement.Dispose();
                flowElements.Controls.Remove(UIElement);
                return;
            }
        }

        private void CreateItems()
        {
            foreach (IMusixMenuItem item in Items)
            {
                AddItem(item);
            }
        }

        private void SidebarItem_OnSelect(MusixDynamicSidebarItem item)
        {
            if (item == SelectedItem)
            {
                return;
            }
            else
            {
                SelectedItem = item;
            }
            item.MenuItem.OnSelect();
            OnSelectionChanged?.Invoke(item.MenuItem);
            foreach (MusixDynamicSidebarItem sidebarItem in flowElements.Controls.OfType<MusixDynamicSidebarItem>())
            {
                if (sidebarItem != item && sidebarItem.IsSelected)
                {
                    sidebarItem.IsSelected = false;
                    sidebarItem.MenuItem.OnDeselect();
                }
            }
        }

        public void SendStyle(EStyle Style)
        {
            this.Style = Style;
            foreach (MusixDynamicSidebarItem item in flowElements.Controls.OfType<MusixDynamicSidebarItem>())
            {
                item.SendStyle(Style);
            }
        }

        private void THoverUpdate_Tick(object sender, System.EventArgs e)
        {
        }

        public void CheckHover(MusixDynamicSidebarItem item, Point ctPoint)
        {
            if (IsInControl(item, ctPoint))
            {
                if (!item.IsHover)
                {
                    item.IsHover = true;
                }
            }
            else
            {
                if (item.IsHover)
                {
                    item.IsHover = false;
                }
            }
        }

        public bool IsInControl(Control ct, Point pt)
        {
            return pt.X >= ct.Location.X && pt.X <= ct.Location.X + ct.Width && pt.Y >= ct.Location.Y && pt.Y <= ct.Location.Y + ct.Height;
        }
    }
}