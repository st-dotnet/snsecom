using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNS.ECommerce.Infrastructure.Interfaces
{
    public interface ICartService
    {
        List<ProductModel> GetProductList();
    }
}
