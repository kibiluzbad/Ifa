using System.Web.Mvc;
using System.Web.Routing;

namespace Ifa.Tests
{
    public class FakeHtmlHelper : HtmlHelper
    {
        public FakeHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : base(viewContext, viewDataContainer)
        {
        }

        public FakeHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection) : base(viewContext, viewDataContainer, routeCollection)
        {
        }

        public FakeHtmlHelper(object model)
            : base(new FakeViewContext(model), new FakeViewDataContainer())
        {
         
        }

        public class FakeViewContext : ViewContext
        {
            public FakeViewContext(object model)
            {
                ViewData = new ViewDataDictionary();
                ViewData.Model = model;
            }
        }

        public class FakeViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }

            public FakeViewDataContainer()
            {
                ViewData = new ViewDataDictionary();
            }
        }
    }
}