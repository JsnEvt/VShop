using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            try
            {
                var categoriesDto = await _categoryService.GetCategories();
                if (categoriesDto == null)
                {
                    return NotFound("Categories not found");
                }
                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar categorias: {ex.Message}");
            }
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            try
            {
                var categoriesDto = await _categoryService.GetCategoriesProducts();
                if (categoriesDto == null)
                {
                    return NotFound("Categories not found");
                }
                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar categorias com produtos: {ex.Message}");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            try
            {
                var categoryDto = await _categoryService.GetCategoryById(id);
                if (categoryDto == null)
                {
                    return NotFound("Category not found");
                }
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar categoria: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Invalid Data");

            try
            {
                await _categoryService.AddCategory(categoryDto);
                return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId }, categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar categoria: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
                return BadRequest("ID mismatch");
            if (categoryDto is null)
                return BadRequest("Invalid data");

            try
            {
                await _categoryService.UpdateCategory(categoryDto);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar categoria: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            try
            {
                var categoryDto = await _categoryService.GetCategoryById(id);
                if (categoryDto == null)
                {
                    return NotFound("Category not found");
                }

                await _categoryService.RemoveCategory(id);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar categoria: {ex.Message}");
            }
        }
    }
}
