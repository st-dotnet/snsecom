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

        public IActionResult UploadFile(IFormFile file)
        {
            var response = _productServices.UploadXLSXFile(file);
            return Json(response);
        }

    }
}
