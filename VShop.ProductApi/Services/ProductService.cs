using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            try
            {
                var productsEntity = await _productRepository.GetAll();
                return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao obter produtos: {ex.Message}", ex);
            }
        }

        public async Task<ProductDTO> GetProductsById(int id)
        {
            try
            {
                var productEntity = await _productRepository.GetById(id);
                if (productEntity == null)
                {
                    throw new Exception("Produto não encontrado.");
                }
                return _mapper.Map<ProductDTO>(productEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao obter produto com ID {id}: {ex.Message}", ex);
            }
        }

        public async Task AddProduct(ProductDTO productDto)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(productDto);
                await _productRepository.Create(productEntity);
                productDto.Id = productEntity.Id;
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao adicionar produto: {ex.Message}", ex);
            }
        }

        public async Task UpdateProduct(ProductDTO productDto)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(productDto);
                await _productRepository.Update(productEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao atualizar produto: {ex.Message}", ex);
            }
        }

        public async Task RemoveProduct(int id)
        {
            try
            {
                var productEntity = await _productRepository.GetById(id);
                if (productEntity == null)
                {
                    throw new Exception("Produto não encontrado.");
                }
                await _productRepository.Delete(productEntity.Id);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao remover produto: {ex.Message}", ex);
            }
        }
    }
}
