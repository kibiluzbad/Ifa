using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using Ifa.Templates;
using Moq;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class CurrentPageTemplateTests
    {
        [Test]
        public void Can_Get_HtmlString_For_CurrentPageTemplate()
        {
            var tagBuilderMock = new Mock<IHtmlTagBuilder>();

            tagBuilderMock
                .Setup(c => c.Build(It
                                        .Is<IDictionary<string, object>>(d => "1" == ""+d["value"]
                                                                              && "currentPage" == ""+d["class"])))
                .Returns("<tag class=\"currentPage\">1</tag>")
                .Verifiable();

            var fakeTagBuilder = tagBuilderMock.Object;

            var currentPage = new CurrentPage(1, 3);
            var fakeHtmlHelper = new FakeHtmlHelper(currentPage);

            BasicIfaTemplate currentPageTemplate = new CurrentPageTemplate();
            currentPageTemplate.HtmlTagBuilder = fakeTagBuilder;
            var htmlString = currentPageTemplate.Get(fakeHtmlHelper);

            Assert.That(htmlString, Is.EqualTo("<tag class=\"currentPage\">1</tag>"));
            tagBuilderMock.VerifyAll();
        }
    }
}
