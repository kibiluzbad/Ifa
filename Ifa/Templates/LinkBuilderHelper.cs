using System.Collections.Generic;
using System.Web;
using System.Web.Mvc.Ajax;
using Ifa.Helpers;

namespace Ifa.Templates
{
    public static class LinkBuilderHelper
    {
        public static string GetAndRemoveValue(ref IDictionary<string, object> htmlAttributes)
        {
            string result = string.Empty;

            if (htmlAttributes.ContainsKey("value"))
            {
                result = "" + htmlAttributes["value"];
                htmlAttributes.Remove("value");
            }

            return HttpUtility.HtmlEncode(result);
        }

        public static AjaxOptions GetAjaxOptions(IHasAjaxOptions html)
        {
            return null != html 
                ? html.AjaxOptions 
                : null;
        }
    }
}