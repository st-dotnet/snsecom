using Microsoft.EntityFrameworkCore;
using SNS.ECommerce.Data;
using SNS.ECommerce.Infrastructure.Interfaces;
using SNS.ECommerce.Models;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SNS.ECommerce.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationContext _dbContext;

        public CartService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Add Products Items Into cart
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool AddItemToCart(ProductModel product)
        {
            var cartItem = _dbContext.ShoppingCart.SingleOrDefault(c => c.ProductId == product.ProductId);
            if (cartItem == null)
            {
                ShoppingCart cartItems = new()
                {
                    ProductId = product.ProductId,
                    DateCreated = DateTime.Now,
                    Count = 1,
                    

                };
                _dbContext.ShoppingCart.Add(cartItems);

            }
            else { cartItem.Count++; }
            _dbContext.SaveChanges();
            return true;
        }
        public List<ProductModel> GetProductList()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ShoppingCart> GetCartItems()
        {
            try
            {
                var data = _dbContext.ShoppingCart.Include(t => t.Product).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public int RemoveFromCart(int Id)
        {
            try
            {
                var data = _dbContext.ShoppingCart.Where(x => x.ProductId == Id).FirstOrDefault();
                int itemCount = 0;
                if (data == null)
                    throw new NullReferenceException();
                if (data != null)
                {
                    if (data.Count > 1)
                    {
                        data.Count--;
                        itemCount = data.Count;
                    }
                    else
                    {
                        _dbContext.ShoppingCart.Remove(data);
                    }
                    _dbContext.SaveChanges();
                }
                return itemCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveCart(int Id)
        {
            try
            {
                var data = _dbContext.ShoppingCart.Where(x => x.CartId == Id).FirstOrDefault();
                if (data == null)
                    throw new NullReferenceException();
                _dbContext.ShoppingCart.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}