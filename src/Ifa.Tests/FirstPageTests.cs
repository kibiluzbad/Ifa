using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class FirstPageTests
    {
        [Test]
        public void FirstPage_With_Items_Per_Page_Equals_10_GetText_Should_Return_First_Page_Text()
        {
            var firstPage = new FirstPage(null, 10);
            var text = firstPage.GetText();

            Assert.That(text, Is.EqualTo("<<"));
        }

        [Test]
        public void FirstPage_Items_Per_Page_Equals_10_GetUrl_Should_Return_Url_To_First_Page()
        {
            var firstPage = new FirstPage((itemPerPage, index) => string.Format("/Controller/Action/{0}/{1}", itemPerPage, index), 10);
            var text = firstPage.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/1"));
        }
    }
}
