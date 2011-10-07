using System.Web.Mvc;
using Ifa.Model;

namespace Ifa.Templates
{
    public interface IIfaTemplate
    {
        string Get(HtmlHelper html);
    }
}