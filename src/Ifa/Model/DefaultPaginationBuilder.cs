using System;
using System.Linq;

namespace Ifa.Model
{
    public class DefaultPaginationBuilder : PaginationBuilder
    {
        public DefaultPaginationBuilder(PagedResultBase pagedResult, Func<int,int,string> urlAction)
            : base(pagedResult,urlAction)
        { }

        public override void BuildFirstPageLink()
        {
            if (IsNotFirstPage())
            {
                _pagination.Add(new FirstPage(_urlAction,
                                              _pagedResult.ItemsPerPage));
            }
        }

        public override void BuildPreviousPageLink()
        {
            if (IsNotFirstPage())
            {
                _pagination.Add(new PreviousPage(_pagedResult.PageNumber, 
                                                 _urlAction,
                                                 _pagedResult.ItemsPerPage));
            }
        }

        public override void BuildPageLinks()
        {
            if (HasPages())
            {
                for (int i = 1; i <= _pagedResult.Pages; i++)
                {
                    if (LeftOuter(i, _pagedResult.Left) || 
                        RightOuter(_pagedResult.Pages, i, _pagedResult.Right) || 
                        InsideWindow(_pagedResult.PageNumber, i, _pagedResult.Window))
                        _pagination.Add(_pagedResult.PageNumber != i
                                            ? CreatePageLink(i)
                                            : CreateCurrentPageLink(i));
                    else if (LastPageWasNotTruncated())
                        _pagination.Add(CreateNoPage());
                }
            }
        }

        public override void BuildNextPageLink()
        {
            if (IsNotLastPage())
            {
                _pagination.Add(new NextPage(_pagedResult.PageNumber,
                                             _urlAction,
                                             _pagedResult.Pages,
                                             _pagedResult.ItemsPerPage));
            }
        }

        public override void BuildLastPageLink()
        {
            if (IsNotLastPage())
            {
                _pagination.Add(new LastPage(_urlAction,
                                             _pagedResult.Pages,
                                             _pagedResult.ItemsPerPage));
            }
        }
    }
}