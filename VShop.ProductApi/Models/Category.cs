namespace VShop.ProductApi.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    //Relacao 1 para muitos - 1 categoria pode ter muitos produtos.
    public ICollection<Product>? Products { get; set; }
}
