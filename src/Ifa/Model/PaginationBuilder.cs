using System;
using System.Collections.Generic;
using System.Linq;

namespace Ifa.Model
{
    public abstract class PaginationBuilder
    {
        protected ICollection<Tag> _pagination;
        protected readonly PagedResultBase _pagedResult;
        protected readonly Func<int, int, string> _urlAction;

        public virtual IEnumerable<Tag> Pagination
        {
            get { return _pagination; }
        }

        protected PaginationBuilder()
        {}

        protected PaginationBuilder(PagedResultBase pagedResult,
            Func<int, int, string> urlAction)
        {
            _pagination = new List<Tag>();
            _pagedResult = pagedResult;
            _urlAction = urlAction;
        }

        public abstract void BuildFirstPageLink();
        public abstract void BuildPreviousPageLink();
        public abstract void BuildPageLinks();
        public abstract void BuildNextPageLink();
        public abstract void BuildLastPageLink();

        protected virtual bool IsNotFirstPage()
        {
            return 1 < _pagedResult.PageNumber;
        }

        protected virtual bool HasPages()
        {
            return 1 < _pagedResult.Pages;
        }

        protected virtual bool LastPageWasNotTruncated()
        {
            return !(_pagination.LastOrDefault() is Gap);
        }

        protected static Gap CreateNoPage()
        {
            return new Gap();
        }

        protected virtual bool IsNotLastPage()
        {
            return _pagedResult.Pages > _pagedResult.PageNumber;
        }

        protected virtual Tag CreateCurrentPageLink(int i)
        {
            return new CurrentPage(i,
                                   _pagedResult.ItemsPerPage);
        }

        protected virtual Tag CreatePageLink(int i)
        {
            return new PageLink(i,
                                _urlAction,
                                _pagedResult.ItemsPerPage);
        }

        protected static bool LeftOuter(int page, int left)
        {
            return page <= left;
        }

        protected static bool RightOuter(int pages, int page, int right)
        {
            return (pages - page) < right;
        }

        protected static bool InsideWindow(int currentPage, int page, int window)
        {
            return Math.Abs(currentPage - page) <= window;
        }
    }
}