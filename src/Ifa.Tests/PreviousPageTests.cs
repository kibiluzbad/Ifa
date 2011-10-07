using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class PreviousPageTests
    {
        [Test]
        public void PreviousPage_With_Current_Equals_2_And_Items_Per_Page_Equals_10_GetText_Should_Return_Previous_Page_Text()
        {
            var previousPage = new PreviousPage(2, null, 2);
            var text = previousPage.GetText();

            Assert.That(text, Is.EqualTo("<"));
        }

        [Test]
        public void PreviousPage_With_Items_Per_Page_Equals_10_And_Current_Equals_2_GetUrl_Should_Return_Url_To_Page_1()
        {
            var previousPage = new PreviousPage(2, (pages, index) => string.Format("/Controller/Action/{0}/{1}", pages, index), 10);
            var text = previousPage.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/1"));
        }

        [Test]
        public void PreviousPage_With_Current_Greater_Than_Pages_GetUrl_Return_Link_To_First_Page()
        {
            var previousPage = new PreviousPage(0, (pages, index) => string.Format("/Controller/Action/{0}/{1}", pages, index), 10);
            var text = previousPage.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/1"));
        }
    }
}
