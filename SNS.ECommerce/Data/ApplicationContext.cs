using Microsoft.EntityFrameworkCore;
using SNS.ECommerce.Models;
using SNS.ECommerce.Web.Models;


namespace SNS.ECommerce.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }

    }
}
