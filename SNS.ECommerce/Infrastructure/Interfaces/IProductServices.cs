using Microsoft.AspNetCore.Http;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;

namespace SNS.ECommerce.Infrastructure.Interfaces
{
    public interface IProductServices
    {
        List<ProductModel> GetProductList();

        bool UploadXLSXFile(IFormFile file); 
    }
}
