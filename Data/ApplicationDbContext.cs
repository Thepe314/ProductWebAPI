using Microsoft.EntityFrameworkCore;

namespace IMS.PRODUCTAPI.Data
{
   public class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
    }

}