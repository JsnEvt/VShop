using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            try
            {
                var result = await _productService.GetAllProducts();
                if (result is null)
                    return View("Error");
                Console.WriteLine("cade os resultados?");

                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar produtos: {ex.Message}");
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            try
            {
                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar categorias para criação de produto: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.CreateProduct(productVM);
                    if (result != null)
                        return RedirectToAction("Index");
                }

                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
                return View(productVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar produto: {ex.Message}");
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            try
            {
                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");

                var result = await _productService.FindProductById(id);
                if (result is null)
                    return View("Error");

                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar produto para atualização: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.UpdateProduct(productVM);
                    if (result is not null)
                        return RedirectToAction("Index");
                }
                return View(productVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.FindProductById(id);
                if (result is null)
                    return View("Error");

                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar produto para exclusão: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost, ActionName("DeleteProduct")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _productService.DeleteProductById(id);
                if (!result)
                    return View("Error");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir produto: {ex.Message}");
                return View("Error");
            }
        }
    }
}
