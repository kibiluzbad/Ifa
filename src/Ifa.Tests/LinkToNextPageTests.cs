using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class LinkToNextPageTests
    {
        [Test]
        public void LinkToNextPage_GetText_Should_Return_The_Correct_Text()
        {
            var linkToNextPage = new LinkToNextPage(1, null, 1, 1);

            var text = linkToNextPage.GetText();

            Assert.That(text, Is.EqualTo("More..."));
        }

        [Test]
        public void LinkToNextPage_GetUrl_With_Pages_Equals_3_Current_Equals_1_Items_Per_Page_Equals_10_Should_Return_Url_For_Page_2()
        {
            var linkToNextPage = new LinkToNextPage(1,
                                                    (itemsPerPage, index) =>
                                                    string.Format("/Controller/Action/{0}/{1}", itemsPerPage, index),
                                                    3,
                                                    10);

            var url = linkToNextPage.GetUrl();

            Assert.That(url, Is.EqualTo("/Controller/Action/10/2"));
        }

        [Test]
        public void LinkToNextPage_GetUrl_With_Pages_Equals_3_Current_Equals_3_Items_Per_Page_Equals_10_Should_Return_Url_For_Page_3()
        {
            var linkToNextPage = new LinkToNextPage(4,
                                                    (itemsPerPage, index) =>
                                                    string.Format("/Controller/Action/{0}/{1}", itemsPerPage, index),
                                                    3,
                                                    10);

            var url = linkToNextPage.GetUrl();

            Assert.That(url, Is.EqualTo("/Controller/Action/10/3"));
        }

        [Test]
        public void LinkToNextPage_GetUrl_With_Pages_Equals_3_Current_Equals_4_Items_Per_Page_Equals_10_Should_Return_Url_For_Page_3()
        {
            var linkToNextPage = new LinkToNextPage(4,
                                                    (itemsPerPage, index) =>
                                                    string.Format("/Controller/Action/{0}/{1}", itemsPerPage, index),
                                                    3,
                                                    10);

            var url = linkToNextPage.GetUrl();

            Assert.That(url, Is.EqualTo("/Controller/Action/10/3"));
        }
    }
}
