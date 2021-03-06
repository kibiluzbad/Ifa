﻿using System;
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
                                                     additionalViewData, ajaxOptions, GetIfaTheme(html));
        }

        private static string GetIfaTheme(HtmlHelper html)
        {
            if (ThereIsATheme(html))
                return "" + html.ViewData["ifaTheme"];
            return null;
        }

        private static bool ThereIsATheme(HtmlHelper html)
        {
            return null != html &&
                   html.ViewData.ContainsKey("ifaTheme");
        }
    }
}