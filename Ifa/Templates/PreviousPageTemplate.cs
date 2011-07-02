﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ifa.Model;

namespace Ifa.Templates
{
    public class PreviousPageTemplate : BasicIfaTemplate
    {
        public PreviousPageTemplate()
        {
            HtmlTagBuilder = new DefaultLinkBuilder();
        }

        public override string Get(HtmlHelper html)
        {
            var tag = GetModel<Tag>(html);

            return HtmlTagBuilder
                .Build(MergeWithJaxaOptions(html, new Dictionary<string, object>
                                                      {
                                                          {"href", tag.GetUrl()},
                                                          {"value", tag.GetText()}
                                                      }));
        }
    }
}
