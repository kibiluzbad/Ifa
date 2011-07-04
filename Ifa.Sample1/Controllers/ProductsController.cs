using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ifa.Model;
using Ifa.Sample1.Models;

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
            [DefaultValue(1)]int currentPage)
        {
            var products = _products.Skip((currentPage - 1)*itemsPerPage).Take(itemsPerPage);
            var result = new PagedResultViewModel<Product>(itemsPerPage, currentPage, _products.Count, products);

            return View(result);
        }

    }
}
