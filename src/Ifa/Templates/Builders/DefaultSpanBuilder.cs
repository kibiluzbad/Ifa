using System.Collections.Generic;
using System.Web.Mvc;
using Ifa.Templates.Helpers;

namespace Ifa.Templates.Builders
{
    public class DefaultSpanBuilder : IHtmlTagBuilder
    {
        public string Build(IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("span")
                                 {
                                     InnerHtml = LinkBuilderHelper.GetAndRemoveValue(ref htmlAttributes)
                                 };

            tagBuilder.MergeAttributes(htmlAttributes);

            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}