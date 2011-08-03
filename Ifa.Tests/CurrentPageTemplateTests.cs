using System;
using System.Linq;
using System.Text;
using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class CurrentPageTemplateTests : BaseTemplateTest
    {
        public CurrentPageTemplateTests()
            : base("<tag class=\"currentPage\">1</tag>")
        { }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new CurrentPageTemplate {HtmlTagBuilder = builder};
        }

        protected override object CreateModel()
        {
            return new CurrentPage(1, 3);
        }

        [Test]
        public void a()
        {
            var c = Math.Abs(1 - 8);
            Assert.AreEqual(7,c);
        }
    }
}
