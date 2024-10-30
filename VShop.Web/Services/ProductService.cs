using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _options;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            try
            {
                var client = _clientFactory.CreateClient("ProductApi");
                var response = await client.GetAsync(apiEndpoint);
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using var apiResponse = await response.Content.ReadAsStreamAsync();
                        var productsVM = await JsonSerializer
                            .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
                        return productsVM;
                    }

                    return Enumerable.Empty<ProductViewModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter produtos: {ex.Message}");
                return Enumerable.Empty<ProductViewModel>();
            }
        }

        public async Task<ProductViewModel> FindProductById(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("ProductApi");
                using (var response = await client.GetAsync(apiEndpoint + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStreamAsync();
                        var productVM = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                        return productVM;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao encontrar produto por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
        {
            try
            {
                var client = _clientFactory.CreateClient("ProductApi");
                StringContent content = new StringContent(JsonSerializer.Serialize(productVM),
                                        Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(apiEndpoint, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStreamAsync();
                        var createdProduct = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                        return createdProduct;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar produto: {ex.Message}");
                return null;
            }
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
        {
            try
            {
                var client = _clientFactory.CreateClient("ProductApi");
                StringContent content = new StringContent(JsonSerializer.Serialize(productVM),
                                        Encoding.UTF8, "application/json");

                using (var response = await client.PutAsync(apiEndpoint + productVM.Id, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStreamAsync();
                        var productUpdated = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                        return productUpdated;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteProductById(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("ProductApi");
                using (var response = await client.DeleteAsync(apiEndpoint + id))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar produto: {ex.Message}");
                return false;
            }
        }
    }
}
