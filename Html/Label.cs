using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bc.Web.Mvc.Html
{
    public class Label
    {
        public Label()
        {
            Type = LabelWidgetType.Default;
        }

        public Label(string text)
        {
            this.Text = text;
            Type = LabelWidgetType.Default;
        }

        public Label(string text, LabelWidgetType type)
        {
            Text = text;
            Type = type;
        }

        public string Text { get; set; }

        public LabelWidgetType Type { get; set; }
    }
}
