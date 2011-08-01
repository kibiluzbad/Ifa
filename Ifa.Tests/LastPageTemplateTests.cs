using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class LastPageTemplateTests : BaseTemplateTest
    {
        public LastPageTemplateTests()
            : base("<a href=\"/10/3\">Last</a>")
        { }

        protected override object CreateModel()
        {
            return new LastPage((index, itensPerPage) => string.Format("/{0}/{1}", itensPerPage, index),3, 10);
        }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new LastPageTemplate() { HtmlTagBuilder = builder };
        }
    }
}