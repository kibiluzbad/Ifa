using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class PageLinkTests
    {
        [Test]
        public void PageLink_With_Index_And_Items_Per_Pages_Equals_10_GetText_Should_Return_1()
        {
            var pageLink = new PageLink(1,null,10);
            var text = pageLink.GetText();

            Assert.That(text, Is.EqualTo("1"));
        }

        [Test]
        public void PageLink_With_Index_And_Items_Per_Pages_Equals_10_GetUrl_Should_Return_With_The_Url_Formated_By_Specified_Func()
        {
            var pageLink = new PageLink(1, (pages,index) => string.Format("/Controller/Action/{0}/{1}",pages,index), 10);
            var text = pageLink.GetUrl();

            Assert.That(text, Is.EqualTo("/Controller/Action/10/1"));
        }
    }
}
