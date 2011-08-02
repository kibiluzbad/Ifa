using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.UI.WebControls;
using Ifa.Helpers;

namespace Ifa.Templates.Renders
{
    public class DefaultTemplateRender : ITemplateRender
    {
        public string Render(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode readOnly, object additionalViewData, AjaxOptions ajaxOptions)
        {
            return IfaTemplateHelpers.TemplateHelper(html, metadata, htmlFieldName, templateName, readOnly,
                                                     additionalViewData, ajaxOptions, GetIfaTemplate(html));
        }

        private static string GetIfaTemplate(HtmlHelper html)
        {
            if (ThereIsATemplate(html))
                return "" + html.ViewData["ifaTemplate"];
            return null;
        }

        private static bool ThereIsATemplate(HtmlHelper html)
        {
            return null != html &&
                   html.ViewData.ContainsKey("ifaTemplate");
        }
    }
}