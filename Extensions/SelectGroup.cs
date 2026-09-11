using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bc.Web.Mvc
{
    public static class SelectGroupExtensions
    {
        //public static IEnumerable<GroupedSelectListItem> ToGroupedSelectList<TEntity>(this IEnumerable<TEntity> collection,
        //    string groupValueMember, string groupDisplayMember, Func<TEntity, bool> selected = null,
        //    string valueMember = null, string displayMember = null,
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

        //    return list.Select(p => new GroupedSelectListItem()
        //    {
        //        GroupKey = Convert.ToString(p.GetType().GetProperty(groupValueMember).GetValue(p, null)),
        //        GroupName = Convert.ToString(p.GetType().GetProperty(groupDisplayMember).GetValue(p, null)),
        //        Text = Convert.ToString(p.GetType().GetProperty(displayMember).GetValue(p, null)),
        //        Value = Convert.ToString(p.GetType().GetProperty(valueMember).GetValue(p, null))
        //    });
        //}

        //public static IEnumerable<GroupedSelectListItem> ToGroupedSelectList<TEntity>(this Repository<TEntity> repository,
        //    string groupValueMember, string groupDisplayMember,
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<TEntity, bool> selected = null, string valueMember = null, string displayMember = null, bool includeNullItem = false) where TEntity : EntityBase
        //{
        //    var list = repository.Get(filter).OrderBy(p => p.DescriptionName).ToList();
        //    return ToGroupedSelectList(list, groupValueMember, groupDisplayMember, selected, valueMember, displayMember, includeNullItem: includeNullItem);
        //}

    }
}
