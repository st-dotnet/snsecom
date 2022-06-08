using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNS.ECommerce.Infrastructure.Interfaces
{
    public interface IProductServices
    {
        //Get all products
        List<ProductModel> GetProductList();

        //Get product by id
        ProductModel GetProductById(int id);

        //Upload csv file
        public bool UploadCSVFile(IFormFile postedFile);

        //Update product by id
        bool UpdateProductById(ProductModel model);

        //Delete Product by id
        bool DeleteProductById(int id);

        
      
    }
}
