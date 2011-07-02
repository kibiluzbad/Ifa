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
    public class DefaultPaginationBuilderTests
    {
        [Test]
        public void Can_Build_First_Page_Tag_For_Page_Greater_Than_First_Page()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(2).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildFirstPageLink();

            var firstPage = builder.Pagination.FirstOrDefault();

            Assert.That(firstPage, Is.Not.Null
                                       .And
                                       .TypeOf<FirstPage>());

            mockPagedResult.Verify();

        }

        [Test]
        public void When_Building_First_Page_Tag_For_Page_Index_Equals_First_Page_The_Will_Not_Be_Builded()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildFirstPageLink();

            var firstPage = builder.Pagination.FirstOrDefault();

            Assert.That(firstPage, Is.Null);

            mockPagedResult.Verify();

        }

        [Test]
        public void Build_First_Page_Tag_For_10_Items_Per_Page_Must_Return_10_Item_Per_Page_On_Url()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.ItemsPerPage).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", itemPerPage,
                                                                                   index));
            builder.BuildFirstPageLink();

            var firstPage = builder.Pagination.FirstOrDefault();

            Assert.That(firstPage.GetUrl(), Is.EqualTo("/Controller/Action/10/1"));

            mockPagedResult.Verify();
        }

        [Test]
        public void Can_Build_Previous_Page_Tag_For_Page_Greater_Than_First_Page()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(2).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildPreviousPageLink();

            var previousPage = builder.Pagination.FirstOrDefault();

            Assert.That(previousPage, Is.Not.Null
                                       .And
                                       .TypeOf<PreviousPage>());

            mockPagedResult.Verify();

        }

        [Test]
        public void When_Building_Previous_Page_Tag_For_Page_Index_Equals_First_Page_It_Will_Not_Be_Builded()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildPreviousPageLink();

            var previousPage = builder.Pagination.FirstOrDefault();

            Assert.That(previousPage, Is.Null);

            mockPagedResult.Verify();
        }

        [Test]
        public void Build_Previous_Page_Tag_For_10_Items_Per_Page_Must_Return_10_Item_Per_Page_On_Url()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.ItemsPerPage).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", itemPerPage,
                                                                                   index));
            builder.BuildPreviousPageLink();

            var previousPage = builder.Pagination.FirstOrDefault();

            Assert.That(previousPage.GetUrl(), Is.EqualTo("/Controller/Action/10/1"));

            mockPagedResult.Verify();
        }

        [Test]
        public void Can_Build_Page_Links_For_3_Pages_Pagination()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.Window).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildPageLinks();

            Assert.That(builder.Pagination.Count(), Is.EqualTo(3));
            
            foreach (var pageLink in builder.Pagination)
            {
                Assert.That(pageLink, Is.Not.Null);
            }

            mockPagedResult.Verify();
        }

        [Test]
        public void When_Building_Page_Links_For_3_Pages_Pagination_Current_Page_Must_Be_Of_CurrentPage_Type()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.Window).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildPageLinks();

            Assert.That(builder.Pagination.Count(), Is.EqualTo(3));

            bool first = true;
            foreach (var pageLink in builder.Pagination)
            {
                if (first)
                {
                    Assert.That(pageLink, Is.Not.Null
                                              .And
                                              .TypeOf<CurrentPage>());
                    first = false;
                }
                else
                {
                    Assert.That(pageLink, Is.Not.Null
                                              .And
                                              .TypeOf<PageLink>());
                }
            }

            mockPagedResult.Verify();
        }

        [Test]
        public void When_Build_Page_Link_With_One_Page_No_Link_Should_Be_Builded()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.Pages).Returns(1).Verifiable();
            
            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildPageLinks();

            Assert.That(builder.Pagination.Count(), Is.EqualTo(0));

            mockPagedResult.Verify();
        }

        [Test]
        public void Build_Page_Link_Tag_For_10_Items_Per_Page_Must_Return_10_Item_Per_Page_On_Url()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.Window).Returns(10).Verifiable();
            mockPagedResult.SetupGet(c => c.ItemsPerPage).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", itemPerPage,
                                                                                   index));
            builder.BuildPageLinks();

            var i = 2;
            foreach (var pageLink in builder.Pagination.Where(c => c is PageLink))
            {
                Assert.That(pageLink.GetUrl(),
                    Is.EqualTo(string.Format("/Controller/Action/10/{0}", i)));
            }

            mockPagedResult.Verify();
        }

        [Test]
        public void Can_Build_Next_Page_Tag_For_Page_Lesser_Than_Last_Page()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildNextPageLink();

            var nextPage = builder.Pagination.FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null
                                       .And
                                       .TypeOf<NextPage>());

            mockPagedResult.Verify();

        }

        [Test]
        public void When_Building_Next_Page_Tag_For_Page_Index_Equals_Last_Page_It_Will_Not_Be_Builded()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(3).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildNextPageLink();

            var nextPage = builder.Pagination.FirstOrDefault();

            Assert.That(nextPage, Is.Null);

            mockPagedResult.Verify();
        }

        [Test]
        public void Build_Next_Page_Tag_For_10_Items_Per_Page_Must_Return_10_Item_Per_Page_On_Url()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.ItemsPerPage).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", itemPerPage,
                                                                                   index));
            builder.BuildNextPageLink();

            var nextPage = builder.Pagination.FirstOrDefault();

            Assert.That(nextPage.GetUrl(), Is.EqualTo("/Controller/Action/10/2"));

            mockPagedResult.Verify();
        }

        [Test]
        public void Can_Build_Last_Page_Tag_For_Page_Lesser_Than_Last_Page()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildLastPageLink();

            var lastPage = builder.Pagination.FirstOrDefault();

            Assert.That(lastPage, Is.Not.Null
                                       .And
                                       .TypeOf<LastPage>());

            mockPagedResult.Verify();

        }

        [Test]
        public void When_Building_Last_Page_Tag_For_Page_Index_Equals_Last_Page_It_Will_Not_Be_Builded()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(3).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(3).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (pages, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", pages,
                                                                                   index));
            builder.BuildLastPageLink();

            var lastPage = builder.Pagination.FirstOrDefault();

            Assert.That(lastPage, Is.Null);

            mockPagedResult.Verify();
        }

        [Test]
        public void Build_Last_Page_Tag_For_10_Items_Per_Page_Must_Return_10_Item_Per_Page_On_Url()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(2).Verifiable();
            mockPagedResult.SetupGet(c => c.ItemsPerPage).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}", itemPerPage,
                                                                                   index));
            builder.BuildLastPageLink();

            var lastPage = builder.Pagination.FirstOrDefault();

            Assert.That(lastPage.GetUrl(), Is.EqualTo("/Controller/Action/10/2"));

            mockPagedResult.Verify();
        }

        [Test]
        public void BuildPageLinks_Can_Truncate_Right_When_Pages_Is_Greater_Than_Window_Size()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(1).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(12).Verifiable();
            mockPagedResult.SetupGet(c => c.Window).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}",
                                                                                   itemPerPage,
                                                                                   index));
            builder.BuildPageLinks();

            var gap = builder.Pagination.FirstOrDefault(c => c is Gap);
            
            Assert.That(gap, Is.Not.Null
                                 .And
                                 .InstanceOf<Gap>());

            mockPagedResult.Verify();
        }

        [Test]
        public void BuildPageLinks_Can_Truncate_Left_When_Pages_Is_Greater_Than_Window_Size_And_Current_Page_Is_Near_To_The_End()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(10).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(12).Verifiable();
            mockPagedResult.SetupGet(c => c.Window).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}",
                                                                                   itemPerPage,
                                                                                   index));
            builder.BuildPageLinks();

            var gap = builder.Pagination.FirstOrDefault(c => c is Gap);

            Assert.That(gap, Is.Not.Null
                                 .And
                                 .InstanceOf<Gap>());

            mockPagedResult.Verify();
        }

        [Test]
        public void BuildPageLinks_Can_Truncate_Left_And_Right_When_Current_Page_Is_Around_The_Middle_Of_Possible_Pages()
        {
            var mockPagedResult = new Mock<PagedResultBase>();
            mockPagedResult.SetupGet(c => c.PageNumber).Returns(9).Verifiable();
            mockPagedResult.SetupGet(c => c.Pages).Returns(20).Verifiable();
            mockPagedResult.SetupGet(c => c.Window).Returns(10).Verifiable();

            PagedResultBase pagedResult = mockPagedResult.Object;

            PaginationBuilder builder = new DefaultPaginationBuilder(pagedResult,
                                                                     (itemPerPage, index) =>
                                                                     string.Format("/Controller/Action/{0}/{1}",
                                                                                   itemPerPage,
                                                                                   index));
            builder.BuildPageLinks();

            var gapCount = builder.Pagination.Count(c => c is Gap);

            Assert.That(gapCount, Is.EqualTo(2));

            mockPagedResult.Verify();
        }
    }
}