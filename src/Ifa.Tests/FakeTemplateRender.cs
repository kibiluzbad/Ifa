using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.UI.WebControls;
using Ifa.Templates;
using Ifa.Templates.Renders;

namespace Ifa.Tests
{
    public class FakeTemplateRender : ITemplateRender
    {
        public string Render(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode readOnly, object additionalViewData, AjaxOptions ajaxOptions)
        {
            return "";
        }
    }
}