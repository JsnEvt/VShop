namespace VShop.ProductApi.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock {  get; set; }
    public string? ImageURL { get; set; }
    //Existe uma relacao da classe Category com produtos entao,
    //faz=-se necessario a adicao de um atributo aqui:
    public Category? Category { get; set; }
    //para tornar mais explicito, informa-se a linha abaixo:
    public int CategoryId { get; set; }

}
