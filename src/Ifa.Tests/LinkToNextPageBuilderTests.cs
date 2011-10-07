using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Model;
using Moq;
using NUnit.Framework;

namespace Ifa.Tests
{
    [TestFixture]
    public class LinkToNextPageBuilderTests
    {
        [Test]
        public void Building_Link_To_Next_Page_For_3_Pages_At_First_Page_Should_Return_LinkToNextPage()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.ItemsPerPage).Returns(10).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new LinkToNextPageBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildNextPageLink();

            var linkToNextPage = builder.Pagination.FirstOrDefault();

            Assert.That(linkToNextPage, Is.Not.Null
                                       .And
                                       .TypeOf<LinkToNextPage>());

            mockPagedResult.Verify();
        }

        [Test]
        public void Building_Link_To_Next_Page_For_3_Pages_At_Last_Page_Should_Return_Null()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(3).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new LinkToNextPageBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildNextPageLink();

            var linkToNextPage = builder.Pagination.FirstOrDefault();

            Assert.That(linkToNextPage, Is.Null);

            mockPagedResult.Verify();
        }

        [Test]
        public void Calling_Other_Build_Methods_Than_BuildNextPageLink_Should_Return_Empty_Tag_List()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            
            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new LinkToNextPageBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildFirstPageLink();
            builder.BuildLastPageLink();
            builder.BuildPageLinks();
            builder.BuildPreviousPageLink();

            var tagsCount = builder.Pagination.Count();

            Assert.That(tagsCount, Is.EqualTo(0));

            mockPagedResult.Verify();
        }
    }
}
