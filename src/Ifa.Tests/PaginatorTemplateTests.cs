using System;
using System.Collections.Generic;
using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class PaginatorTemplateTests : BaseTemplateTest
    {
        public PaginatorTemplateTests()
            : base("<nav class=\"paginator\"><ul><span class=\"current\">1</span><a href=\"/10/2\">2</a><a href=\"/10/2\">Next</a><a href=\"/10/2\">Last</a>")
        { }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new PaginatorTemplate
                       {
                           HtmlTagBuilder = builder,
                           TemplateRender = new FakeTemplateRender()
                       };
        }

        protected override object CreateModel()
        {
            return new List<Tag>
                       {
                           new CurrentPage(1, 2),
                           new PageLink(2, UrlBuilder, 10),
                           new NextPage(1, UrlBuilder, 2, 10),
                           new LastPage(UrlBuilder, 2, 10)
                       };
        }

        private static string UrlBuilder(int index, int itensPerPage)
        {
            return string.Format("/{0}/{1}", index, itensPerPage);
        }
    }
}