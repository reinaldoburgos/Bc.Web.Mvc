using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Bc.Web.Mvc
{
    public static class SelectListExtensions
    {

        //public static SelectList ToSelectList(this IEnumerable<Bc.Web.Mvc.Helper.GeneralValues> collection,
        //    Func<Bc.Web.Mvc.Helper.GeneralValues, bool> selected = null,
        //    bool includeNullItem = false
        //    )
        //{
        //    var list = collection.ToList();

        //    if (includeNullItem)
        //    {
        //        Bc.Web.Mvc.Helper.GeneralValues entity = new Helper.GeneralValues();
        //        list.Insert(0, entity);
        //    }

        //    Helper.GeneralValues selectedValue = null;
        //    if (selected != null)
        //        selectedValue = list.Where(selected).SingleOrDefault();

        //    return new SelectList(list, "Code", "Value",
        //        selectedValue == null ? null : selectedValue.GetType().GetProperty("Code").GetValue(selectedValue));
        //}

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection,
            string valueMember, string displayMember, Func<T, bool> selected = null,
            bool includeNullItem = false
            )
        {
            var list = collection.ToList();

            if (includeNullItem)
            {
                T entity = (T)Activator.CreateInstance(typeof(T));
                list.Insert(0, entity);
            }

            T selectedValue = default(T);
            if (selected != null)
                selectedValue = list.Where(selected).SingleOrDefault();

            return new SelectList(list, valueMember, displayMember,
                selectedValue == null ? null : selectedValue.GetType().GetProperty(valueMember).GetValue(selectedValue));
        }

        //public static SelectList ToSelectList<TEntity>(this IEnumerable<TEntity> collection,
        //    Func<TEntity, bool> selected = null, string valueMember = null, string displayMember = null,
        //    bool includeNullItem = false
        //    ) where TEntity : EntityBase
        //{
        //    var list = collection.ToList();

        //    if (includeNullItem)
        //    {
        //        TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
        //        list.Insert(0, entity);
        //    }

        //    TEntity selectedValue = null;
        //    if (selected != null)
        //        selectedValue = list.Where(selected).SingleOrDefault();

        //    if (string.IsNullOrEmpty(valueMember))
        //        valueMember = "Key";
        //    if (string.IsNullOrEmpty(displayMember))
        //        displayMember = "DescriptionName";

        //    return new SelectList(list, valueMember, displayMember, selectedValue == null ? null : (object)selectedValue.Key);
        //}

        //public static SelectList ToSelectList<TEntity>(this Repository<TEntity> repository,
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<TEntity, bool> selected = null, string valueMember = null, string displayMember = null, bool includeNullItem = false) where TEntity : EntityBase
        //{
        //    var list = repository.Get(filter).OrderBy(p => p.DescriptionName).ToList();
        //    return ToSelectList(list, selected, includeNullItem: includeNullItem);
        //}

        public static SelectList ToSelectList(this Enum enumeration)
        {
            var source = Enum.GetValues(enumeration.GetType());

            var items = new Dictionary<int, string>();

            foreach (var value in source)
            {
                FieldInfo field = value.GetType().GetField(value.ToString());

                DisplayAttribute attrs = (DisplayAttribute)field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                if (attrs != null)
                {
                    items.Add(Convert.ToInt32(value), attrs.GetName());
                }
                else
                {
                    DescriptionPropertyAttribute attr2s = (DescriptionPropertyAttribute)field.GetCustomAttributes(typeof(DescriptionPropertyAttribute), false).FirstOrDefault();
                    if (attr2s != null)
                        items.Add(Convert.ToInt32(value), attr2s.Description);
                    else
                    {
                        DescriptionAttribute attr3s = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                        if (attr3s != null)
                            items.Add(Convert.ToInt32(value), attr3s.Description);
                    }

                }
            }

            return new SelectList(items, "Key", "Value", (Convert.ToInt32(enumeration)));
        }
    }
}
