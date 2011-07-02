using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ifa.Helpers;
using System.Linq;

namespace Ifa.Templates
{
    public abstract class BasicIfaTemplate : IIfaTemplate
    {
        public abstract string Get(HtmlHelper html);
        public IHtmlTagBuilder HtmlTagBuilder { get; set; }

        protected virtual T GetModel<T>(HtmlHelper html) where T : class
        {
            var result = html.ViewContext.ViewData.Model as T;

            if (null == result)
                throw new InvalidOperationException(string.Format("The model provided ins't of type {0}.",
                                                                  typeof (T).Name));

            return result;
        }

        protected virtual IDictionary<string,object> MergeWithJaxaOptions(HtmlHelper html, IDictionary<string,object> attributes)
        {
            var ajaxOptions = LinkBuilderHelper.GetAjaxOptions(html as IHasAjaxOptions);
            if (null != ajaxOptions)
            {
                return attributes
                    .Union(ajaxOptions.ToUnobtrusiveHtmlAttributes())
                    .ToDictionary(c=>c.Key, c=>c.Value);
            }
            return attributes;
        }
    }
}