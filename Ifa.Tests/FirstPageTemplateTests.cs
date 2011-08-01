using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class FirstPageTemplateTests : BaseTemplateTest
    {
        public FirstPageTemplateTests()
            : base("<a href=\"/10/1\">First</a>")
        { }

        protected override object CreateModel()
        {
            return new FirstPage((index,itensPerPage)=> string.Format("/{0}/{1}",itensPerPage,index),10);
        }

        protected override BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder)
        {
            return new FirstPageTemplate { HtmlTagBuilder = builder };
        }
    }
}