using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class NextPageTemplateTests : BaseTemplateTest
    {
        public NextPageTemplateTests()
            : base("<a href=\"/10/2\">Next</a>")
        { }

        protected override object CreateModel()
        {
            return new NextPage(1,(index, itensPerPage) => string.Format("/{0}/{1}", itensPerPage, index), 3, 10);
        }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new NextPageTemplate { HtmlTagBuilder = builder };
        }
    }
}