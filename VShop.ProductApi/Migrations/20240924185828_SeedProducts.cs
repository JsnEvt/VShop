using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId) " + 
                "Values('Caderno', 20, 'Caderno_simples', 10, 'caderno1.jgp', 1)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId) " +
                "Values('Lapis', 5, 'Lapis_preto', 20, 'lapis1.jgp', 1)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId) " +
                "Values('Clips', 10, 'Cx_clips', 30, 'clips1.jgp', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from Products");
        }
    }
}
