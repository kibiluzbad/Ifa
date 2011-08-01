using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class PreviousPageTemplateTests : BaseTemplateTest
    {
        public PreviousPageTemplateTests()
            : base("<a href=\"/10/2\">Previous</a>")
        { }

        protected override object CreateModel()
        {
            return new NextPage(3, (index, itensPerPage) => string.Format("/{0}/{1}", itensPerPage, index), 3, 10);
        }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new PreviousPageTemplate { HtmlTagBuilder = builder };
        }
    }
}