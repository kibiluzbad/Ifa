﻿using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ifa.Model;

namespace Ifa.Helpers
{
    public static class PaginatorExtensions
    {
        public static MvcHtmlString Paginate<TModel>(this HtmlHelper<TModel> htmlHelper,
            PagedResultBase pagedResult, 
            Func<int, int, string> urlFunc,
            AjaxOptions ajaxOptions = null)
        {
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            if (null == pagedResult) throw new ArgumentNullException("pagedResult");
            if (null == urlFunc) throw new ArgumentNullException("urlFunc");

            return DoPagination(htmlHelper,
                ajaxOptions,
                new DefaultPaginationBuilder(pagedResult,urlFunc));
        }

        public static MvcHtmlString LinkToNextPage<TModel>(this HtmlHelper<TModel> htmlHelper,
            PagedResultBase pagedResult,
            Func<int, int, string> urlFunc,
            AjaxOptions ajaxOptions = null)
        {
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            if (null == pagedResult) throw new ArgumentNullException("pagedResult");
            if (null == urlFunc) throw new ArgumentNullException("urlFunc");

            return DoPagination(htmlHelper,
                   ajaxOptions,
                   new LinkToNextPageBuilder(pagedResult, urlFunc));
        }

        internal static MvcHtmlString DoPagination<TModel>(HtmlHelper<TModel> htmlHelper,
            AjaxOptions ajaxOptions,
            PaginationBuilder builder)
        {
            var paginator = new Paginator();

            paginator.Construct(builder);

            return htmlHelper.DisplayFor(c => builder.Pagination,ajaxOptions);
        }
    }
}
