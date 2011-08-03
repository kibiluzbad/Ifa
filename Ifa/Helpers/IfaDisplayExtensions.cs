using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace Ifa.Helpers
{
    internal static class IfaDisplayExtensions
    {
        public static MvcHtmlString Display(this HtmlHelper html, string expression)
        {
            return IfaTemplateHelpers.Template(html, expression, null /* templateName */, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null);
        }

        public static MvcHtmlString Display(this HtmlHelper html, string expression, object additionalViewData)
        {
            return IfaTemplateHelpers.Template(html, expression, null /* templateName */, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, additionalViewData,null,null);
        }

        public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName)
        {
            return IfaTemplateHelpers.Template(html, expression, templateName, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null);
        }

        public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName, object additionalViewData)
        {
            return IfaTemplateHelpers.Template(html, expression, templateName, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, additionalViewData,null,null);
        }

        public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName, string htmlFieldName)
        {
            return IfaTemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null);
        }

        public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            return IfaTemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, additionalViewData,null,null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return IfaTemplateHelpers.TemplateFor(html, expression, null /* templateName */, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        {
            return IfaTemplateHelpers.TemplateFor(html, expression, null /* templateName */, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, additionalViewData, null,null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, AjaxOptions ajaxOptions, string ifaTheme = null)
        {
            return html.TemplateFor(expression, null, null, DataBoundControlMode.ReadOnly, null, ajaxOptions, ifaTheme);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, AjaxOptions ajaxOptions, string ifaTheme = null)
        {
            return html.TemplateFor(expression, templateName, null, DataBoundControlMode.ReadOnly, null, ajaxOptions, ifaTheme);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
        {
            return IfaTemplateHelpers.TemplateFor(html, expression, templateName, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, additionalViewData, null,null);
        }
        
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            return IfaTemplateHelpers.TemplateFor(html, expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, additionalViewData,null,null);
        }

        public static MvcHtmlString DisplayForModel(this HtmlHelper html)
        {
            return MvcHtmlString.Create(IfaTemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, null /* templateName */, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null));
        }

        public static MvcHtmlString DisplayForModel(this HtmlHelper html, object additionalViewData)
        {
            return MvcHtmlString.Create(IfaTemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, null /* templateName */, DataBoundControlMode.ReadOnly, additionalViewData,null,null));
        }

        public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName)
        {
            return MvcHtmlString.Create(IfaTemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, templateName, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null));
        }

        public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName, object additionalViewData)
        {
            return MvcHtmlString.Create(IfaTemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, templateName, DataBoundControlMode.ReadOnly, additionalViewData,null,null));
        }

        public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName, string htmlFieldName)
        {
            return MvcHtmlString.Create(IfaTemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.ReadOnly, null /* additionalViewData */,null,null));
        }

        public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName, string htmlFieldName, object additionalViewData)
        {
            return MvcHtmlString.Create(IfaTemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.ReadOnly, additionalViewData,null,null));
        }
    }
}
