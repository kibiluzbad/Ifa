using System.Collections.Generic;

namespace Ifa.Templates
{
    public interface IHtmlTagBuilder
    {
        string Build(IDictionary<string,object> htmlAttributes);
    }
}