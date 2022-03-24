using System;

namespace Bc.Web.Mvc.Html
{
    public class MvcContent : Bc.Web.Mvc.Utility.DisposableHelper
    {
        public MvcContent(Action begin, Action end)
            : base(begin, end)
        {
        }
    }
}
