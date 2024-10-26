using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            try
            {
                var categoriesEntity = await _categoryRepository.GetAll();
                return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao obter categorias: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            try
            {
                var categoriesEntity = await _categoryRepository.GetCategpriesProducts();
                return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao obter categorias com produtos: {ex.Message}", ex);
            }
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            try
            {
                var categoryEntity = await _categoryRepository.GetById(id);
                return _mapper.Map<CategoryDTO>(categoryEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao obter categoria com ID {id}: {ex.Message}", ex);
            }
        }

        public async Task AddCategory(CategoryDTO categoryDto)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.Create(categoryEntity);
                categoryDto.CategoryId = categoryEntity.CategoryId;
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao adicionar categoria: {ex.Message}", ex);
            }
        }

        public async Task UpdateCategory(CategoryDTO categoryDto)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.Update(categoryEntity);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao atualizar categoria: {ex.Message}", ex);
            }
        }

        public async Task RemoveCategory(int id)
        {
            try
            {
                var categoryEntity = await _categoryRepository.GetById(id);
                if (categoryEntity == null)
                {
                    throw new Exception("Categoria não encontrada.");
                }
                await _categoryRepository.Delete(categoryEntity.CategoryId);
            }
            catch (Exception ex)
            {
                // Lógica de log pode ser adicionada aqui
                throw new Exception($"Erro ao remover categoria: {ex.Message}", ex);
            }
        }
    }
}
