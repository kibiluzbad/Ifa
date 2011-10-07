using System.Collections.Generic;
using Ifa.Model;
using Ifa.Templates;
using Ifa.Templates.Builders;
using Moq;
using NUnit.Framework;

namespace Ifa.Tests
{
    public abstract class BaseTemplateTest
    {
        private readonly string _expectedTag;

        protected BaseTemplateTest(string expectedTag)
        {
            _expectedTag = expectedTag;
        }

        [Test]
        public virtual void Can_Get_HtmlString_For_Template()
        {
            var tagBuilderMock = new Mock<IHtmlTagBuilder>();

            tagBuilderMock
                .Setup(c => c.Build(It.IsAny<IDictionary<string, object>>()))
                .Returns(_expectedTag)
                .Verifiable();

            var fakeTagBuilder = tagBuilderMock.Object;

            var model = CreateModel();
            var fakeHtmlHelper = new FakeHtmlHelper(model);

            var currentPageTemplate = GetTemplate(fakeTagBuilder);

            var htmlString = currentPageTemplate.Get(fakeHtmlHelper);

            Assert.That(htmlString, Is.EqualTo(_expectedTag));
            tagBuilderMock.VerifyAll();
        }

        protected abstract BasicIfaTemplate GetTemplate(IHtmlTagBuilder builder);


        protected abstract object CreateModel();
    }
}