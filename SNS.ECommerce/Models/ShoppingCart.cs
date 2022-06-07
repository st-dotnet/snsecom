using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SNS.ECommerce.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
      
    }
}
