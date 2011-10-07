using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class PageLinkTemplateTests : BaseTemplateTest
    {
        public PageLinkTemplateTests()
            : base("<a href=\"/10/1\">1</a>")
        { }

        protected override object CreateModel()
        {
            return new PageLink(1, (index, itensPerPage) => string.Format("/{0}/{1}", itensPerPage, index), 10);
        }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new PageLinkTemplate { HtmlTagBuilder = builder };
        }
    }
}