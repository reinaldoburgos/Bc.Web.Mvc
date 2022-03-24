using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bc.Web.Mvc
{
    public static class MultiSelectListExtensions
    {
        //public static MultiSelectList ToMultiSelectList<TEntity>(this Repository<TEntity> repository,
        //    Expression<Func<TEntity, bool>> filter = null, IEnumerable selectedValues = null,
        //    string valueMember = null, string displayMember = null) where TEntity : EntityBase
        //{
        //    var list = repository.Get(filter).ToList();
        //    if (string.IsNullOrEmpty(valueMember))
        //        valueMember = "Key";
        //    if (string.IsNullOrEmpty(displayMember))
        //        displayMember = "DescriptionName";
        //    return new MultiSelectList(list, valueMember, displayMember, selectedValues);
        //}

        //public static MultiSelectList ToMultiSelectList<TEntity>(this IEnumerable<TEntity> collection,
        //    IEnumerable selectedValues = null, string valueMember = null, string displayMember = null) where TEntity : EntityBase
        //{
        //    var list = collection.ToList();

        //    if (string.IsNullOrEmpty(valueMember))
        //        valueMember = "Key";
        //    if (string.IsNullOrEmpty(displayMember))
        //        displayMember = "DescriptionName";

        //    return new MultiSelectList(list, valueMember, displayMember, selectedValues);
        //}

        public static MultiSelectList ToMultiSelectList<T>(this IEnumerable<T> collection,
            string valueMember, string displayMember, Func<T, bool> selected = null)
        {

            var list = collection.ToList();

            if (string.IsNullOrEmpty(valueMember))
                valueMember = "Key";
            if (string.IsNullOrEmpty(displayMember))
                displayMember = "DescriptionName";

            List<object> selectedValues = new List<object>();
            if (selected != null)
            {
                var selecteds = list.Where(selected);

                foreach (var sel in selecteds)
                {
                    object selectedValue = sel.GetType().GetProperty(valueMember).GetValue(sel);
                    selectedValues.Add(selectedValue);
                }
            }

            return new MultiSelectList(list, valueMember, displayMember, selectedValues);
        }

        //public static MultiSelectList ToMultiSelectList(this IEnumerable<Bc.Web.Mvc.Helper.GeneralValues> collection,
        //   Func<Bc.Web.Mvc.Helper.GeneralValues, bool> selected = null)
        //{
        //    return ToMultiSelectList(collection, "Code", "Value", selected);
        //}
    }
}
