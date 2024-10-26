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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            try
            {
                var productDto = await _productService.GetProducts();
                if (productDto == null)
                {
                    return NotFound("Products not found");
                }
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                // Log do erro, caso necessário
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar produtos: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            try
            {
                var productDto = await _productService.GetProductsById(id);
                if (productDto == null)
                {
                    return NotFound("Product not found");
                }
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar o produto: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Data Invalid");

            try
            {
                await _productService.AddProduct(productDto);
                return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar produto: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Data Invalid");

            try
            {
                await _productService.UpdateProduct(productDto);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar produto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            try
            {
                var productDto = await _productService.GetProductsById(id);
                if (productDto == null)
                {
                    return NotFound("Product not found");
                }

                await _productService.RemoveProduct(id);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar produto: {ex.Message}");
            }
        }
    }
}
