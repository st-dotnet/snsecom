using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SNS.ECommerce.Infrastructure.Interfaces;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace SNS.ECommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
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

        /// <summary>
        /// Upload csv file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult UploadFile(IFormFile file)
        {
            var response = _productServices.UploadCSVFile(file);
            return Json(response);
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
        /// Delete product by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Delete(int Id)
        {
            var response = _productServices.DeleteProducts(Id);
            return Json(response);
        }
        /// <summary>
        /// Create product 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductModel productModel)
        {
            return Json(_productServices.AddProduct(productModel));
        }
       
    }
}
