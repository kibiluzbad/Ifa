using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.UI.WebControls;

namespace Ifa.Templates.Renders
{
    public interface ITemplateRender
    {
        string Render(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode readOnly, object additionalViewData, AjaxOptions ajaxOptions);
    }
}