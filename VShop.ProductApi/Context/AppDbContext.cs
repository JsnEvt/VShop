using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context
{
    public class AppDbContext : DbContext //para definir o mapeamento objeto-relacional
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        //definindo o mapeamento entre as entidades:
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
