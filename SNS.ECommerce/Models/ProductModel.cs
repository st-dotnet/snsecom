using System.ComponentModel.DataAnnotations;

namespace SNS.ECommerce.Web.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }

        public string SKU { get; set; }

        public string Price { get; set; }

        public int Quantity { get; set; }

        public bool ShowAvailability { get; set; }

        public string Description { get; set; }

        public string url { get; set; }
    }
}