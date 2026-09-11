using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bc.Web.Mvc.Html
{
    public class TabCollection : List<TabPane>
    {
        public TabCollection()
        {
        }

        public TabPane Get(string key)
        {
            return this.FirstOrDefault(p => p.Key == key);
        }

        public TabPane GetSelected()
        {
            return this.FirstOrDefault(p => p.Selected);
        }

        public new void Add(TabPane tab)
        {
            tab.TabCollection = this;
            base.Add(tab);
        }

        public void Add(string key)
        {
            TabPane tab = new TabPane(key);
            this.Add(tab);
        }

        public void Add(string key, TabTarget targetType, string name)
        {
            TabPane tab = new TabPane(key, targetType, name);
            this.Add(tab);
        }

        public void Add(string key, TabTarget targetType, string name, bool selected)
        {
            TabPane tab = new TabPane(key, targetType, name, selected);
            this.Add(tab);
        }

        public void Add(string key, TabTarget targetType, string target, string name)
        {
            TabPane tab = new TabPane(key, targetType, target, name);
            this.Add(tab);
        }

        public void Add(string key, TabTarget targetType, string target, string name, bool selected = false, string icon = null, string iconColor = null, string id = null)
        {
            TabPane tab = new TabPane(key, targetType, target, name, selected, icon, iconColor, id);
            this.Add(tab);
        }

        public new void Remove(TabPane tab)
        {
            tab.TabCollection = null;
            base.Remove(tab);
        }

        public void Remove(string key)
        {
            this.RemoveAll(p => p.Key == key);
        }
    }

    public class TabPane
    {
        public TabPane(string key)
        {
            this.Key = key;
            TargetType = TabTarget.HtmlElement;
            this.Target = key;
        }

        public TabCollection TabCollection { get; set; }

        public TabPane(string key, TabTarget targetType, string name)
        {
            this.Target = key;
            this.Key = key;
            this.Name = name;
            this.TargetType = targetType;
        }


        public TabPane(string key, TabTarget targetType, string name, bool selected)
            : this(key, targetType, name)
        {
            this.Selected = selected;
        }

        public TabPane(string key, TabTarget targetType, string target, string name)
        {
            this.Target = target;
            this.TargetType = targetType;
            this.Key = key;
            this.Name = name;
        }

        public TabPane(string key, TabTarget targetType, string target, string name, bool selected, string icon = null, string iconColor = null, string id = null)
        {
            this.Key = key;
            this.TargetType = targetType;
            this.Target = target;
            this.Selected = selected;
            this.Name = name;
            this.Icon = icon;
            this.IconColor = iconColor;
            this.Id = id;
        }

        public string ResultTarget
        {
            get
            {
                switch (this.TargetType)
                {
                    case TabTarget.Url:
                        return Target;
                    case TabTarget.HtmlElement:
                        return "#" + Key;
                }
                return null;
            }
        }

        public string Target { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Id { get; set; }

        private bool selected = false;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (value == true && TabCollection != null)
                {
                    foreach (TabPane tab in TabCollection)
                    {
                        tab.selected = false;
                    }
                }
                selected = value;
            }
        }

        public TabTarget TargetType { get; set; }
    }

    public enum TabTarget
    {
        Url,
        HtmlElement
    }
}
