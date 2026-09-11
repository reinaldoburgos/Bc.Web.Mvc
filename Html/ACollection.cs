using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bc.Web.Mvc.Html
{
    public class ACollection: List<AItem>
    {
        //public NavItem GetCurrent()
        //{
        //    return this.FirstOrDefault(p => p.Current);
        //}

    }

    public class AItem
    {
        public AItem(string href, string title, string onClick, string iconClass)
        {
            Href = href;
            Title = title;
            OnClick = onClick;
            IconClass = iconClass;
        }
    
        public string Href { get; set; }
        public string Title { get; set; }
        public string OnClick { get; set; }
        public string IconClass { get; set; }
    }
}
