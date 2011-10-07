using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            AjaxOptions ajaxOptions = null,
            string ifaTheme = null)
        {
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            if (null == pagedResult) throw new ArgumentNullException("pagedResult");
            if (null == urlFunc) throw new ArgumentNullException("urlFunc");

            return DoPagination(htmlHelper,
                ajaxOptions,
                new DefaultPaginationBuilder(pagedResult,urlFunc),
                ifaTheme);
        }

        public static MvcHtmlString LinkToNextPage<TModel>(this HtmlHelper<TModel> htmlHelper,
            PagedResultBase pagedResult,
            Func<int, int, string> urlFunc,
            AjaxOptions ajaxOptions = null,
            string ifaTemplate = null)
        {
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            if (null == pagedResult) throw new ArgumentNullException("pagedResult");
            if (null == urlFunc) throw new ArgumentNullException("urlFunc");

            return DoPagination(htmlHelper,
                   ajaxOptions,
                   new LinkToNextPageBuilder(pagedResult, urlFunc),
                   ifaTemplate);
        }


        public static MvcHtmlString DisplayForTheme<TModel>(this HtmlHelper<TModel> html, Tag model)
        {
            var ajaxOptions = html.ViewData["ajaxOptions"] as AjaxOptions;
            var theme = ""+html.ViewData["ifaTheme"];

            if (model is FirstPage)
                return html.DisplayForTheme(model as FirstPage, ajaxOptions, theme);
            if (model is PreviousPage)
                return html.DisplayForTheme(model as PreviousPage, ajaxOptions, theme);
            if (model is CurrentPage)
                return html.DisplayForTheme(model as CurrentPage, ajaxOptions, theme);
            if (model is PageLink)
                return html.DisplayForTheme(model as PageLink, ajaxOptions, theme);
            if (model is NextPage)
                return html.DisplayForTheme(model as NextPage, ajaxOptions, theme);
            if (model is LastPage)
                return html.DisplayForTheme(model as LastPage, ajaxOptions, theme);
            if (model is Gap)
                return html.DisplayForTheme(model as Gap, ajaxOptions, theme);

            throw new InvalidOperationException(string.Format("I don't know this tag \"{0}\"", model.GetType().FullName));
        }

        internal static MvcHtmlString DisplayForTheme<TModel, TValue>(this HtmlHelper<TModel> html, TValue model, AjaxOptions ajaxOptions, string theme)
         {
             return html.DisplayFor(c => model, ajaxOptions, theme); 
         }

        internal static MvcHtmlString DoPagination<TModel>(HtmlHelper<TModel> htmlHelper,
            AjaxOptions ajaxOptions,
            PaginationBuilder builder,
            string ifaTheme)
        {
            var paginator = new Paginator();

            paginator.Construct(builder);

            return htmlHelper.DisplayFor(c => builder.Pagination,"Paginator",ajaxOptions,ifaTheme);
        }
    }
}
