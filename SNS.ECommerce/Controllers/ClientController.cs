using Microsoft.AspNetCore.Mvc;
using SNS.ECommerce.Infrastructure.Interfaces;
using System.Linq;

namespace SNS.ECommerce.Controllers
{
    public class ClientController : Controller
    {
        private readonly IProductServices _productServices;

        public ClientController(IProductServices productServices)
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