using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SNS.ECommerce.Infrastructure.Interfaces;
using SNS.ECommerce.Web.Models;
using System.Linq;

namespace SNS.ECommerce.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly INotyfService _notyf;

        private readonly IProductServices _productServices;
        private IHostingEnvironment _Environment;

        public ProductController(IProductServices productServices, IHostingEnvironment Environment, INotyfService notyf)
        {
            _productServices = productServices;
            _Environment = Environment;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            //Get products list
            var products = _productServices.GetProductList().ToList();
            ViewBag.ProductList = products;
            return View();
        }

        /// <summary>
        /// Upload csv file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFile()
        {
            //file upload process
            var file = Request.Form.Files[0];
            return Json(_productServices.UploadCSVFile(file)); 
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var response = _productServices.GetProductById(id);
            return Json(response);
        }

        /// <summary>
        /// Update product by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateProduct(ProductModel model)
        {
            return Json(_productServices.UpdateProductById(model));
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            return Json(_productServices.DeleteProductById(id));

        }       
    }
}