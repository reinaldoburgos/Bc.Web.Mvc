using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class SelectExtensions
    {
        public static MvcHtmlString BcDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null, bool useAjax = false)
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
               new
               {
                   @class = Constants.Style.ElementClass.SelectClass,
                   Bc_select = useAjax ? "use-ajax" : "",
                   place_holder = "Seleccione un Item"
               },
               htmlAttributes);

            string editor = htmlHelper.DropDownListFor(expression, selectList, htmlAttributes: editorAttr).ToString();

            string validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }

        //TODO: Importante
        //public static MvcHtmlString BcDropDownListIdentificationTypeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
        //    TProperty>> expression, object htmlAttributes = null, bool usePlaceHolder = false)
        //{
        //    var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
        //           new { place_holder = Bc.Resources.LegendFor.SelectAnItem },
        //           htmlAttributes);

        //    GeneralProxy proxy = new GeneralProxy();
        //    return htmlHelper.BcDropDownListFor(expression, proxy.GetIdentificationTypes().ToSelectList("IdentificationTypeId", "Name", includeNullItem: usePlaceHolder),
        //        editorAttr);
        //}

        //todo: importante
        //public static MvcHtmlString BcDropDownListNaturalPersonIdentificationTypeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
        //    TProperty>> expression, object htmlAttributes = null, bool usePlaceHolder = false)
        //{
        //    var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
        //           new { place_holder = Bc.Resources.LegendFor.SelectAnItem },
        //           htmlAttributes);

        //    GeneralProxy proxy = new GeneralProxy();
        //    return htmlHelper.BcDropDownListFor(expression, proxy.GetIdentificationTypesForNaturalPerson().ToSelectList("IdentificationTypeId", "Name", includeNullItem: usePlaceHolder),
        //        editorAttr);
        //}

        //todo: importante
        //public static MvcHtmlString BcDropDownListLegalEntityIdentificationTypeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
        //    TProperty>> expression, object htmlAttributes = null, bool usePlaceHolder = false)
        //{
        //    var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
        //           new { place_holder = Bc.Resources.LegendFor.SelectAnItem },
        //           htmlAttributes);

        //    GeneralProxy proxy = new GeneralProxy();
        //    return htmlHelper.BcDropDownListFor(expression, proxy.GetIdentificationTypesForLegalEntity().ToSelectList("IdentificationTypeId", "Name", includeNullItem: usePlaceHolder),
        //        editorAttr);
        //}

        //todo: importante
        //public static MvcHtmlString BcDropDownListGeneralValuesFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
        //    TProperty>> expression, short id, object htmlAttributes = null, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null)
        //{
        //    Proxies.GeneralProxy genaralProxy = new Proxies.GeneralProxy();

        //    if (sortexpression == null)
        //        return htmlHelper.BcDropDownListFor(expression, genaralProxy.GetGeneralValuesById(id).ToSelectList("Code", "Content"), htmlAttributes);
        //    else
        //        return htmlHelper.BcDropDownListFor(expression, genaralProxy.GetGeneralValuesById(id).OrderBy(sortexpression).ToSelectList("Code", "Content"), htmlAttributes);
        //}

        //todo: importante
        //public static MvcHtmlString BcDropDownListGeneralValues(this HtmlHelper htmlHelper, string name, short id, object htmlAttributes = null, string defaultValue = null, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null)
        //{
        //    Proxies.GeneralProxy genaralProxy = new Proxies.GeneralProxy();

        //    if (sortexpression == null)
        //        return htmlHelper.BcDropDownList(genaralProxy.GetGeneralValuesById(id).ToSelectList("Code", "Content", p => p.Code == defaultValue, includeNullItem: true), htmlAttributes, name: name);
        //    else
        //        return htmlHelper.BcDropDownList(genaralProxy.GetGeneralValuesById(id).OrderBy(sortexpression).ToSelectList("Code", "Content", p => p.Code == defaultValue, includeNullItem: true), htmlAttributes, name: name);
        //}

        public static MvcHtmlString BcDropDownList(this HtmlHelper htmlHelper,
            IEnumerable<SelectListItem> selectList, object htmlAttributes = null, string name = "")
        {
            IDictionary<string, object> editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
               new { @class = Constants.Style.ElementClass.SelectClass, Bc_select = "", place_holder = "Seleccione un Item" },
               htmlAttributes);

            string editor = htmlHelper.DropDownList(name, selectList, htmlAttributes: htmlAttributes).ToString();

            return MvcHtmlString.Create(editor.ToString());
        }

        public static MvcHtmlString BcDropDownListTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression, object htmlAttributes = null, int interval = 30)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            DateTime StartDateTime = new DateTime(1900, 1, 1);

            int max = (60 / interval) * 24;

            for (int i = 1; i <= max; i++)
            {
                items.Add(new SelectListItem()
                {
                    Value = StartDateTime.Ticks.ToString(),
                    Text = string.Format("{0:HH:mm}", StartDateTime)
                });
                StartDateTime = StartDateTime.AddMinutes(interval);
            }

            var result = htmlHelper.BcDropDownListFor(expression, items, htmlAttributes);
            return result;
        }

        public static MvcHtmlString BcDropDownListTime(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, int interval = 30)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            DateTime StartDateTime = new DateTime(1900, 1, 1);

            int max = (60 / interval) * 24;

            for (int i = 1; i <= max; i++)
            {
                items.Add(new SelectListItem()
                {
                    Value = StartDateTime.Ticks.ToString(),
                    Text = string.Format("{0:HH:mm}", StartDateTime)
                });
                StartDateTime = StartDateTime.AddMinutes(interval);
            }

            var result = htmlHelper.BcDropDownList(items, htmlAttributes, name);
            return result;
        }

        public static MvcHtmlString BcDropDownListDurationFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression, object htmlAttributes = null, string selected = "30")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int min = 15; min <= 300; min += 5)
            {
                string display = string.Empty;
                if (min > 60)
                    display = string.Format("{0} {1} {2} min", min / 60, min / 60 > 1 ? "hrs" : "hr", min - (min / 60) * 60);
                else
                    display = string.Format("{0} min", min);
                items.Add(new SelectListItem() { Value = min.ToString(), Text = display, Selected = min.ToString() == selected });
            }

            var result = htmlHelper.BcDropDownListFor(expression, items, htmlAttributes);
            return result;
        }

        public static MvcHtmlString BcDropDownListDayOfMonthListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
           TProperty>> expression, object htmlAttributes = null, string selectedValue = "0")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem()
            {
                Value = null,
                Text = ""
            });

            for (int i = 1; i <= 31; i++)
            {
                items.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            var result = htmlHelper.DropDownListFor(expression, items, htmlAttributes);
            return result;
        }

        public static MvcHtmlString BcDropDownListDayOfMonthList(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, string selectedValue = "0")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem()
            //{
            //    Value = null,
            //    Text = ""
            //});

            for (int i = 1; i <= 31; i++)
            {
                items.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                    Selected = (selectedValue == i.ToString())
                });
            }

            var result = htmlHelper.BcDropDownList(items, htmlAttributes, name);
            return result;
        }

        public static MvcHtmlString BcDropDownListYearListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression, object htmlAttributes = null, string selectedValue = "0")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem()
            {
                Value = null,
                Text = ""
            });

            for (int year = Bc.Web.Mvc.Session.CurrentDateTime.Year;
                    year >= Bc.Web.Mvc.Session.CurrentDateTime.Year - 110; year--)
            {
                items.Add(new SelectListItem()
                {
                    Value = year.ToString(),
                    Text = year.ToString()
                });
            }

            var result = htmlHelper.DropDownListFor(expression, items, htmlAttributes);
            return result;
        }

        public static MvcHtmlString BcDropDownListMonthListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression, object htmlAttributes = null, string selectedValue = "0")
        {
            var newitems = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat
                .MonthNames
                .Select((monthName, index) => new SelectListItem
                {
                    Value = (index + 1).ToString(),
                    Text = monthName,
                    Selected = (selectedValue == (index + 1).ToString())
                });

            var result = htmlHelper.DropDownListFor(expression, newitems, htmlAttributes);
            return result;
        }

        public static MvcHtmlString BcDropDownListMonthList(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, string selectedValue = "0")
        {
            var newitems = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat
                .MonthNames
                .Select((monthName, index) => new SelectListItem
                {
                    Value = (index + 1).ToString(),
                    Text = monthName,
                    Selected = (selectedValue == (index + 1).ToString())
                });

            var result = htmlHelper.BcDropDownList(newitems, htmlAttributes, name);
            return result;
        }
    }
}
