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
    }
}
