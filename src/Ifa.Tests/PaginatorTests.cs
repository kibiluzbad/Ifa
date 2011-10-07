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
    public class PaginatorTests
    {
        [Test]
        public void Assert_That_Paginator_Call_All_Builder_Methods_Once()
        {
            var mockBuilder = new Mock<PaginationBuilder>();

            mockBuilder.Setup(c => c.BuildFirstPageLink()).Verifiable();
            mockBuilder.Setup(c => c.BuildLastPageLink()).Verifiable();
            mockBuilder.Setup(c => c.BuildNextPageLink()).Verifiable();
            mockBuilder.Setup(c => c.BuildPageLinks()).Verifiable();
            mockBuilder.Setup(c => c.BuildPreviousPageLink()).Verifiable();
        
            var builder = mockBuilder.Object;

            var paginator = new Paginator();
            paginator.Construct(builder);

            mockBuilder.Verify(c=>c.BuildFirstPageLink(), Times.Once());
            mockBuilder.Verify(c=>c.BuildLastPageLink(), Times.Once());
            mockBuilder.Verify(c=>c.BuildNextPageLink(), Times.Once());
            mockBuilder.Verify(c=>c.BuildPageLinks(), Times.Once());
            mockBuilder.Verify(c=>c.BuildPreviousPageLink(), Times.Once());
            mockBuilder.Verify(c=>c.Pagination, Times.Never());
        }

    }
}
