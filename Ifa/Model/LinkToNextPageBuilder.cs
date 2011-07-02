using System;

namespace Ifa.Model
{
    public class LinkToNextPageBuilder : PaginationBuilder
    {
        public LinkToNextPageBuilder(PagedResultBase pagedResult, 
                                     Func<int, int, string> urlAction) 
            : base(pagedResult, urlAction)
        {
        }

        public override void BuildFirstPageLink()
        {
            
        }

        public override void BuildPreviousPageLink()
        {
            
        }

        public override void BuildPageLinks()
        {
            
        }

        public override void BuildNextPageLink()
        {
            if (IsNotLastPage())
            {
                _pagination.Add(new LinkToNextPage(_pagedResult.PageNumber,
                                                   _urlAction,
                                                   _pagedResult.Pages,
                                                   _pagedResult.ItemsPerPage));
            }
        }

        
        public override void BuildLastPageLink()
        {
            
        }
    }
}