using Microsoft.AspNetCore.Http;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;

namespace SNS.ECommerce.Infrastructure.Interfaces
{
    public interface IProductServices
    {
        //Get all products
        List<ProductModel> GetProductList();

        //Get product by id
        ProductModel GetProductById(int id);

        //Upload csv file
        bool UploadCSVFile(IFormFile file);

        //Update product by id
        bool UpdateProductById(ProductModel model);

        //delete product by id
        bool DeleteProducts(int Id);

        //Add product
        bool AddProduct(ProductModel productModel);
    }
}
