﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ifa.Model;

namespace Ifa.Templates
{
    public class GapTemplate : BasicIfaTemplate
    {
        public GapTemplate()
        {
            HtmlTagBuilder = new DefaultSpanBuilder();
        }
        public override string Get(HtmlHelper html)
        {
            var tag = GetModel<Tag>(html);
            return HtmlTagBuilder
                .Build(MergeWithJaxaOptions(html, new Dictionary<string, object>
                                                      {
                                                          {"value", tag.GetText()},
                                                          {"class", "gap"}
                                                      }));
        }
    }
}
