using Microsoft.AspNetCore.Mvc;
using SNS.ECommerce.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNS.ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductServices _productServices;
        public CartController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public IActionResult Index()
        {
            //Get products list
            var products = _productServices.GetProductList().ToList();
            ViewBag.ProductList = products;
            return View();
        }
    }
}
