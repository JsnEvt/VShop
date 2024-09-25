using System.ComponentModel.DataAnnotations;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required(ErrorMessage ="The price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The description is required")]
        [MinLength(5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage ="The stock is required")]
        [Range(1, 999)]
        public long Stock { get; set; }
        public string? ImageURL { get; set; }
        //Existe uma relacao da classe Category com produtos entao,
        //faz=-se necessario a adicao de um atributo aqui:
        public Category? Category { get; set; }
        //para tornar mais explicito, informa-se a linha abaixo:
        public int CategoryId { get; set; }
    }
}
