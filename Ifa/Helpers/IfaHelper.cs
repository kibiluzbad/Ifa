using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Ajax;

namespace Ifa.Helpers
{
    public static class IfaHelper
    {
        public static string GetAjaxAttributes(AjaxOptions options)
        {
            if (null == options) return string.Empty;

            var dictionary = options.ToUnobtrusiveHtmlAttributes();
            return string.Join(" ", dictionary
                                        .Select(c => string.Format("{0}={1}", c.Key, c.Value))
                                        .ToArray());
        }

        public static bool IsFirstPage(this IEnumerable<Model.Tag> tags)
        {
            return tags.Any(c => c is Model.FirstPage);
        }

        public static bool IsLastPage(this IEnumerable<Model.Tag> tags)
        {
            return tags.Any(c => c is Model.LastPage);
        }

        public static IEnumerable<Model.Tag> RemoveTagsByType(this IEnumerable<Model.Tag> tags,params Type[] types)
        {
            return tags.Where(tag => !types.Contains(tag.GetType()));
        }
    }
}
