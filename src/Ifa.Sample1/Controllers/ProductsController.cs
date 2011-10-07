using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ifa.Model;
using Ifa.Sample1.Models;
using Ifa.Helpers;

namespace Ifa.Sample1.Controllers
{
    public class ProductsController : Controller
    {
        private ICollection<Product> _products;
        
        public ProductsController()
        {
            InitProducts();
        }

        private void InitProducts()
        {
            _products = new List<Product>();
            for (int i = 1; i <= 300; i++)
            {
                _products.Add(new Product
                                  {
                                      Id = i,
                                      CreatedAt = DateTime.Now,
                                      Name = "Product" + 1,
                                      Value = 105.5M*i
                                  });
            }
        }

        //
        // GET: /Products/
        public ActionResult Index([DefaultValue(10)]int itemsPerPage,
            [DefaultValue(1)]int currentPage,
            [DefaultValue("")]string theme)
        {
            var result = _products.AsQueryable().Page(currentPage);

            ViewBag.Theme = theme;

            return IsAjaxReuqest() ? (ActionResult)PartialView("Products", result) : View(result);
        }

        private bool IsAjaxReuqest()
        {
            return "XMLHttpRequest".Equals(Request.Headers["X-Requested-With"],
                                           StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
