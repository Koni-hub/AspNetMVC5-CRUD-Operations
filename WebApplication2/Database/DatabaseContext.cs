using System.Data.Entity;
using WebApplication2.Models;

namespace WebApplication2.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=ProductDatabaseConnection")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
