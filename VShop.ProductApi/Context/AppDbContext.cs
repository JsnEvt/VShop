﻿using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context
{
    public class AppDbContext : DbContext //para definir o mapeamento objeto-relacional
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        //definindo o mapeamento entre as entidades:
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //Fluent API 
        //Usado para sobrescrever as configuracoes padroes que sao geradas durante as migrations

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //category
            mb.Entity<Category>().HasKey(c => c.CategoryId);
            mb.Entity<Category>().
                Property(c => c.Name).HasMaxLength(100).IsRequired();

            //product
            mb.Entity<Product>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            mb.Entity<Product>().Property(c => c.Description).HasMaxLength(255).IsRequired();
            mb.Entity<Product>().Property(c => c.ImageURL).HasMaxLength(255).IsRequired();
            mb.Entity<Product>().Property(c => c.Price).HasPrecision(12, 2);

            //definindo a relacao de 1 para muitos:
            mb.Entity<Category>().HasMany(g => g.Products).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //populando a tabela category:
            mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Material escolar",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios"
                }
                );
        }
    }
}
