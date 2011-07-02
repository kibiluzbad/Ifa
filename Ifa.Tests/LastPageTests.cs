using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class LastPageTests
    {
        [Test]
        public void LastPage_With_Items_Per_Page_Equals_10_GetText_Should_Return_Last_Page_Text()
        {
            var lastPage = new LastPage(null,2,10);
            var text = lastPage.GetText();

            Assert.That(text, Is.EqualTo(">>"));
        }

        [Test]
        public void LastPage_With_Pages_Equals_2_And_Items_Per_Page_Equals_10_GetUrl_Should_Return_Url_To_Page_2()
        {
            var lastPage = new LastPage((pages, index) => string.Format("/Controller/Action/{0}/{1}", pages, index), 2, 10);
            var text = lastPage.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/2"));
        }
    }
}
