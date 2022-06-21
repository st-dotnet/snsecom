using SNS.ECommerce.Models;
using SNS.ECommerce.Web.Models;
using System.Collections.Generic;

namespace SNS.ECommerce.Infrastructure.Interfaces
{
    public interface ICartService
    {
        List<ProductModel> GetProductList();
        bool AddItemToCart(ProductModel product);
        List<ShoppingCart> GetCartItems();
        int RemoveFromCart(int Id);
        bool RemoveCart(int Id);
    }
}