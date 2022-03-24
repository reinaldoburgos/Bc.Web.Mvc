using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bc.Web.Mvc.Html
{
    public class NavCollection: List<NavItem>
    {
        public NavItem GetCurrent()
        {
            return this.FirstOrDefault(p => p.Current);
        }

    }

    public class NavItem
    {
        public NavItem()
        {
        }

        public NavItem(string urlTarget, string name)
        {
            this.UrlTarget = urlTarget;
            this.Name = name;
        }

        public NavItem(string urlTarget, string name, bool current)
            :this(urlTarget, name)
        {
            this.Current = current;
        }

        public NavItem(string urlTarget, string name, bool current, string iconClass)
            : this(urlTarget, name, current)
        {
            this.IconClass = iconClass;
        }

        public NavItem(string urlTarget, string name, bool current, string iconClass, string tooltip)
            :this(urlTarget,name,current,iconClass)
        {            
            this.ToolTip = tooltip;
        }

        public string Name { get; set; }

        public string ToolTip { get; set; }
        
        public string UrlTarget { get; set; }

        public string IconClass { get; set; }
        
        public bool Current { get; set; }

    }
}
