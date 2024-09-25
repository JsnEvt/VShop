using System.ComponentModel.DataAnnotations;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        //DataAnnotations:
        [Required(ErrorMessage = "The name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        //Relacao 1 para muitos - 1 categoria pode ter muitos produtos.
        public ICollection<Product>? Products { get; set; }
    }
}
