using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.UI.WebControls;
using Ifa.Helpers;
using Ifa.Model;

namespace Ifa.Templates
{
    public class PaginatorTemplate : BasicIfaTemplate
    {
        public override string Get(HtmlHelper html)
        {
            var tags = GetModel<IEnumerable<Tag>>(html);

            var paginator = new StringBuilder("<nav class=\"paginator\">\r\n<ul>\r\n");

            AppendTags(html, tags, paginator);

            paginator.Append("</ul>\r\n</nav>\r\n");

            return paginator.ToString();
        }

        private static void AppendTags(HtmlHelper html, IEnumerable<Tag> tags, StringBuilder paginator)
        {
            foreach (var metadata in
                tags.Select(tag => ModelMetadataProviders.Current.GetMetadataForType(() => tag, tag.GetType())))
            {
                paginator.Append(
                    IfaTemplateHelpers.TemplateHelper(html, metadata, null, null,
                                                      DataBoundControlMode.ReadOnly, null,
                                                      LinkBuilderHelper.GetAjaxOptions(html as IHasAjaxOptions)));
            }
        }
    }
}
