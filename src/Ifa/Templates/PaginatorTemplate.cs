using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Ifa.Helpers;
using Ifa.Model;
using Ifa.Templates.Builders;
using Ifa.Templates.Helpers;
using Ifa.Templates.Renders;

namespace Ifa.Templates
{
    public class PaginatorTemplate : BasicIfaTemplate
    {
        public ITemplateRender TemplateRender { get; set; }
        
        public PaginatorTemplate()
        {
            HtmlTagBuilder = new DefaultPaginatorBuilder();
            TemplateRender = new DefaultTemplateRender();
        }

        public override string Get(HtmlHelper html)
        {
            var tags = GetModel<IEnumerable<Tag>>(html);
            var innerTemplates = GetInnerTemplates(html, tags);

            return HtmlTagBuilder.Build(new Dictionary<string, object>
                                            {
                                                {"innerTemplates", innerTemplates}
                                            });
        }

        private IList<string> GetInnerTemplates(HtmlHelper html, IEnumerable<Tag> tags)
        {
            return
                (from metadata in
                     tags.Select(tag => ModelMetadataProviders
                                            .Current
                                            .GetMetadataForType(() => tag,
                                                                tag.GetType()))
                 let ajaxOptions = LinkBuilderHelper.GetAjaxOptions(html as IHasAjaxOptions)
                 select TemplateRender.Render(html,
                                                          metadata,
                                                          null,
                                                          null,
                                                          DataBoundControlMode.ReadOnly,
                                                          new { ajaxOptions },
                                                          ajaxOptions))
                    .ToList();
        }
    }
}
