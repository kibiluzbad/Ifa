using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ifa.Model;

namespace Ifa.Templates
{
    public class CurrentPageTemplate : BasicIfaTemplate
    {
        public CurrentPageTemplate()
        {
            HtmlTagBuilder = new DefaultSpanBuilder();
        }

        public override string Get(HtmlHelper html)
        {
            var tag = GetModel<Tag>(html);
            return HtmlTagBuilder.Build(new Dictionary<string, object>
                                                                       {
                                                                           {"value", tag.GetText()},
                                                                           {"class", "currentPage"}
                                                                       });
        }
    }
}
