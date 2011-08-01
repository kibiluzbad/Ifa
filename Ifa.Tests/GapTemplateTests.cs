using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class GapTemplateTests : BaseTemplateTest
    {
        public GapTemplateTests()
            : base("<tag class=\"gap\">...<tag>")
        { }

        protected override object CreateModel()
        {
            return new Gap();
        }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new GapTemplate { HtmlTagBuilder = builder };
        }
    }
}