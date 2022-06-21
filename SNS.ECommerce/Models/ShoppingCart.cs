using SNS.ECommerce.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SNS.ECommerce.Models
{
    public class ShoppingCart
    {

        [Key]
        public int CartId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}