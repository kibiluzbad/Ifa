using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class NextPageTests
    {
        [Test]
        public void NextPage_With_Pages_Equals_2_And_Current_Equals_1_And_Items_Per_Page_Equals_10_GetText_Should_Return_Next_Page_Text()
        {
            var nextPage = new NextPage(1,null, 2, 10);
            var text = nextPage.GetText();

            Assert.That(text, Is.EqualTo(">"));
        }

        [Test]
        public void NextPage_With_Pages_Equals_2_And_Current_Equals_1_And_Items_Per_Page_Equals_10_GetUrl_Should_Return_Url_To_Page_2()
        {
            var nextPage = new NextPage(1,
                (pages, index) => string.Format("/Controller/Action/{0}/{1}", pages, index),
                2,
                10);
            var text = nextPage.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/2"));
        }

        [Test]
        public void NextPage_With_Current_Greater_Than_Pages_GetUrl_Return_Link_To_Last_Page()
        {
            var nextPage = new NextPage(3,
            (pages, index) => string.Format("/Controller/Action/{0}/{1}", pages, index),
            2,
            10);
            var text = nextPage.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/2"));
        }

    }
}
