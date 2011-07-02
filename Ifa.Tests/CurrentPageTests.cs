using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class CurrentPageTests
    {
        [Test]
        public void CurrentPage_With_Index_Equals_1_And_Items_Per_Page_Equals_10_GetText_Should_Return_1()
        {
            var currentPage = new CurrentPage(1, 10);
            var text = currentPage.GetText();

            Assert.That(text, Is.EqualTo("1"));
        }

        [Test]
        public void CurrentPage_With_Index_Equals_1_And_Items_Per_Page_Equals_10_GetUrl_Should_Return_String_Empty()
        {
            var currentPage = new CurrentPage(1, 10);
            var text = currentPage.GetUrl();

            Assert.That(text, Is.EqualTo(string.Empty));
        }
    }
}
