using System.Collections.Generic;

namespace Ifa.Templates.Builders
{
    public interface IHtmlTagBuilder
    {
        string Build(IDictionary<string,object> htmlAttributes);
    }
}