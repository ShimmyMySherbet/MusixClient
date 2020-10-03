using System.Collections.Generic;
using System.Linq;
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

        public MusixDynamicSidebar()
        {
            InitializeComponent();
        }

        public void AddItem(IMusixMenuItem item)
        {
            Items.Add(item);
            MusixDynamicSidebarItem sidebarItem = new MusixDynamicSidebarItem(item);
            sidebarItem.SendStyle(Style);
            sidebarItem.OnSelect += SidebarItem_OnSelect;
            flowElements.Controls.Add(sidebarItem);
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
    }
}