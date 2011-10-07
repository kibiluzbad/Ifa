using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ifa.Templates.Builders
{
    public class DefaultPaginatorBuilder : IHtmlTagBuilder
    {
        public string Build(IDictionary<string, object> htmlAttributes)
        {
            var paginator = new StringBuilder("<nav class=\"paginator\">\r\n<ul>\r\n");

            foreach (var item in (IEnumerable)htmlAttributes["innerTemplates"] 
                                 ?? new List<object>())
            {
                paginator.Append(item);
            }

            paginator.Append("</ul>\r\n</nav>\r\n");

            return paginator.ToString();
        }
    }
}