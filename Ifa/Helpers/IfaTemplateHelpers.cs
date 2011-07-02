using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Ifa.Model;
using Ifa.Templates;

namespace Ifa.Helpers
{
    internal static class IfaTemplateHelpers
    {
        internal static string IfaTemplatesPath = "Ifa";
        
        static readonly Dictionary<string, Func<HtmlHelper, string>> DefaultIfaActions =
            new Dictionary<string, Func<HtmlHelper, string>>(StringComparer.OrdinalIgnoreCase) {
                { typeof(CurrentPage).Name, new CurrentPageTemplate().Get },
                { typeof(FirstPage).Name, new FirstPageTemplate().Get },
                { typeof(Gap).Name, new GapTemplate().Get },
                { typeof(LastPage).Name, new LastPageTemplate().Get },
                { typeof(NextPage).Name, new NextPageTemplate().Get },
                { typeof(PageLink).Name, new PageLinkTemplate().Get },
                { typeof(PreviousPage).Name, new PreviousPageTemplate().Get },
                { typeof(IEnumerable<Tag>).Name, new PaginatorTemplate().Get },
                { typeof(LinkToNextPage).Name, new NextPageTemplate().Get },
            };

        
        internal static string cacheItemId = Guid.NewGuid().ToString();

        internal delegate string ExecuteTemplateDelegate(HtmlHelper html, ViewDataDictionary viewData, string templateName, DataBoundControlMode mode, GetViewNamesDelegate getViewNames, AjaxOptions ajaxOptions);

        internal static string ExecuteTemplate(HtmlHelper html, ViewDataDictionary viewData, string templateName, DataBoundControlMode mode, GetViewNamesDelegate getViewNames, AjaxOptions ajaxOptions)
        {
            Dictionary<string, ActionCacheItem> actionCache = GetActionCache(html);
            Dictionary<string, Func<HtmlHelper, string>> defaultActions = DefaultIfaActions;
            string modeViewPath = IfaTemplatesPath;

            foreach (string viewName in getViewNames(viewData.ModelMetadata, templateName, viewData.ModelMetadata.TemplateHint, viewData.ModelMetadata.DataTypeName))
            {
                string fullViewName = modeViewPath + "/" + viewName;
                ActionCacheItem cacheItem;

                if (actionCache.TryGetValue(fullViewName, out cacheItem))
                {
                    if (cacheItem != null)
                    {
                        return cacheItem.Execute(html, viewData, ajaxOptions);
                    }
                }
                else
                {
                    ViewEngineResult viewEngineResult = ViewEngines.Engines.FindPartialView(html.ViewContext, fullViewName);
                    if (viewEngineResult.View != null)
                    {
                        actionCache[fullViewName] = new ActionCacheViewItem { ViewName = fullViewName };

                        using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                        {
                            viewEngineResult.View.Render(new ViewContext(html.ViewContext, viewEngineResult.View, viewData, html.ViewContext.TempData, writer), writer);
                            return writer.ToString();
                        }
                    }

                    Func<HtmlHelper, string> defaultAction;
                    if (defaultActions.TryGetValue(viewName, out defaultAction))
                    {
                        actionCache[fullViewName] = new ActionCacheCodeItem { Action = defaultAction };
                        return defaultAction(MakeHtmlHelper(html, viewData,ajaxOptions));
                    }

                    actionCache[fullViewName] = null;
                }
            }

            throw new InvalidOperationException(
                String.Format(
                    CultureInfo.CurrentCulture,
                    "Unable to locate an appropriate template for type {0}.",
                    viewData.ModelMetadata.ModelType.FullName
                )
            );
        }

        internal static Dictionary<string, ActionCacheItem> GetActionCache(HtmlHelper html)
        {
            HttpContextBase context = html.ViewContext.HttpContext;
            Dictionary<string, ActionCacheItem> result;

            if (!context.Items.Contains(cacheItemId))
            {
                result = new Dictionary<string, ActionCacheItem>();
                context.Items[cacheItemId] = result;
            }
            else
            {
                result = (Dictionary<string, ActionCacheItem>)context.Items[cacheItemId];
            }

            return result;
        }
        
        internal delegate IEnumerable<string> GetViewNamesDelegate(ModelMetadata metadata, params string[] templateHints);

        internal static IEnumerable<string> GetViewNames(ModelMetadata metadata, params string[] templateHints)
        {
            foreach (string templateHint in templateHints.Where(s => !String.IsNullOrEmpty(s)))
            {
                yield return templateHint;
            }

            // We don't want to search for Nullable<T>, we want to search for T (which should handle both T and Nullable<T>)
            Type fieldType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            // TODO: Make better string names for generic types
            yield return fieldType.Name;
        }

        internal static MvcHtmlString Template(HtmlHelper html, string expression, string templateName, string htmlFieldName, DataBoundControlMode mode, object additionalViewData, AjaxOptions ajaxOptions)
        {
            return MvcHtmlString.Create(Template(html, expression, templateName, htmlFieldName, mode, additionalViewData, TemplateHelper, ajaxOptions));
        }

        // Unit testing version
        internal static string Template(HtmlHelper html, string expression, string templateName, string htmlFieldName,
                                        DataBoundControlMode mode, object additionalViewData, TemplateHelperDelegate templateHelper, AjaxOptions ajaxOptions)
        {
            return templateHelper(html,
                                  ModelMetadata.FromStringExpression(expression, html.ViewData),
                                  htmlFieldName ?? ExpressionHelper.GetExpressionText(expression),
                                  templateName,
                                  mode,
                                  additionalViewData,
                                  ajaxOptions);
        }

        internal static MvcHtmlString TemplateFor<TContainer, TValue>(this HtmlHelper<TContainer> html, Expression<Func<TContainer, TValue>> expression,
                                                                      string templateName, string htmlFieldName, DataBoundControlMode mode,
                                                                      object additionalViewData,
                                                                      AjaxOptions ajaxOptions)
        {
            return MvcHtmlString.Create(TemplateFor(html, expression, templateName, htmlFieldName, mode, additionalViewData, TemplateHelper,ajaxOptions));
        }

        // Unit testing version
        internal static string TemplateFor<TContainer, TValue>(this HtmlHelper<TContainer> html, Expression<Func<TContainer, TValue>> expression,
                                                               string templateName, string htmlFieldName, DataBoundControlMode mode,
                                                               object additionalViewData, TemplateHelperDelegate templateHelper, AjaxOptions ajaxOptions)
        {
            return templateHelper(html,
                                  ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                                  htmlFieldName ?? ExpressionHelper.GetExpressionText(expression),
                                  templateName,
                                  mode,
                                  additionalViewData,
                                  ajaxOptions);
        }

        internal delegate string TemplateHelperDelegate(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode mode, object additionalViewData, AjaxOptions ajaxOptions);

        internal static string TemplateHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode mode, object additionalViewData, AjaxOptions ajaxOptions)
        {
            return TemplateHelper(html, metadata, htmlFieldName, templateName, mode, additionalViewData, ExecuteTemplate, ajaxOptions);
        }

        internal static string TemplateHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode mode, object additionalViewData, ExecuteTemplateDelegate executeTemplate, AjaxOptions ajaxOptions)
        {
            // TODO: Convert Editor into Display if model.IsReadOnly is true? Need to be careful about this because
            // the Model property on the ViewPage/ViewUserControl is get-only, so the type descriptor automatically
            // decorates it with a [ReadOnly] attribute...

            if (metadata.ConvertEmptyStringToNull && String.Empty.Equals(metadata.Model))
            {
                metadata.Model = null;
            }

            object formattedModelValue = metadata.Model;
            if (metadata.Model == null && mode == DataBoundControlMode.ReadOnly)
            {
                formattedModelValue = metadata.NullDisplayText;
            }

            string formatString = mode == DataBoundControlMode.ReadOnly ? metadata.DisplayFormatString : metadata.EditFormatString;
            if (metadata.Model != null && !String.IsNullOrEmpty(formatString))
            {
                formattedModelValue = String.Format(CultureInfo.CurrentCulture, formatString, metadata.Model);
            }

            // Normally this shouldn't happen, unless someone writes their own custom Object templates which
            // don't check to make sure that the object hasn't already been displayed
            object visitedObjectsKey = metadata.Model ?? metadata.ModelType ;
            //if (html.ViewDataContainer.ViewData.TemplateInfo.VisitedObjects.Contains(visitedObjectsKey))
            //{    // DDB #224750
            //    return String.Empty;
            //}

            ViewDataDictionary viewData = new ViewDataDictionary(html.ViewDataContainer.ViewData)
            {
                Model = metadata.Model,
                ModelMetadata = metadata,
                TemplateInfo = new TemplateInfo
                {
                    FormattedModelValue = formattedModelValue,
                    HtmlFieldPrefix = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName),
                    //VisitedObjects = new HashSet<object>(html.ViewContext.ViewData.TemplateInfo.VisitedObjects),    // DDB #224750
                }
            };

            if (additionalViewData != null)
            {
                foreach (KeyValuePair<string, object> kvp in new RouteValueDictionary(additionalViewData))
                {
                    viewData[kvp.Key] = kvp.Value;
                }
            }

            //viewData.TemplateInfo.VisitedObjects.Add(visitedObjectsKey);    // DDB #224750

            return executeTemplate(html, viewData, templateName, mode, GetViewNames, ajaxOptions);
        }

        // Helpers

        private static HtmlHelper MakeHtmlHelper(HtmlHelper html, ViewDataDictionary viewData, AjaxOptions ajaxOptions)
        {
            return null != ajaxOptions
                ? new IfaHtmlHelper(
                new ViewContext(html.ViewContext, html.ViewContext.View, viewData, html.ViewContext.TempData, html.ViewContext.Writer),
                new ViewDataContainer(viewData)
            ){AjaxOptions = ajaxOptions}
            : new HtmlHelper(
                new ViewContext(html.ViewContext, html.ViewContext.View, viewData, html.ViewContext.TempData, html.ViewContext.Writer),
                new ViewDataContainer(viewData)
            );
        }

        internal abstract class ActionCacheItem
        {
            public abstract string Execute(HtmlHelper html, ViewDataDictionary viewData, AjaxOptions ajaxOptions);
        }

        internal class ActionCacheCodeItem : ActionCacheItem
        {
            public Func<HtmlHelper, string> Action { get; set; }

            public override string Execute(HtmlHelper html, ViewDataDictionary viewData, AjaxOptions ajaxOptions)
            {
                return Action(MakeHtmlHelper(html, viewData, ajaxOptions));
            }
        }

        internal class ActionCacheViewItem : ActionCacheItem
        {
            public string ViewName { get; set; }

            public override string Execute(HtmlHelper html, ViewDataDictionary viewData, AjaxOptions ajaxOptions)
            {
                ViewEngineResult viewEngineResult = ViewEngines.Engines.FindPartialView(html.ViewContext, ViewName);
                using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    viewEngineResult.View.Render(new ViewContext(html.ViewContext, viewEngineResult.View, viewData, html.ViewContext.TempData, writer), writer);
                    return writer.ToString();
                }
            }
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataContainer(ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }

            public ViewDataDictionary ViewData { get; set; }
        }
    }

    public class IfaHtmlHelper : HtmlHelper , IHasAjaxOptions
    {
        public AjaxOptions AjaxOptions { get; set; }
        
        public IfaHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : base(viewContext, viewDataContainer)
        {
        }

        public IfaHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection) : base(viewContext, viewDataContainer, routeCollection)
        {
        }
    }

    public interface IHasAjaxOptions
    {
        AjaxOptions AjaxOptions { get; }
    }
}
