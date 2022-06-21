using Microsoft.AspNetCore.Http;
using SNS.ECommerce.Web.Models;
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
        //public bool UploadCSVFile(IFormFile postedFile);
        bool UploadCSVFile(IFormFile file);

        //Update product by id
        bool UpdateProductById(ProductModel model);

        //Delete Product by id
        bool DeleteProductById(int id);      
    }
}