using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
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
            var result = await _productService.GetAllProducts();

            if (result is null)
                return View("Error");

            return View(result);
        }

        //O metodo abaixo servira para obter todas as categorias para preencher o campo "combobox"
        //ele carregara o formulario e exibira as categorias que ja estao no banco de dados.
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            //ViewBag equivale ao comboBox
            ViewBag.CategoryId = new SelectList(await
                _categoryService.GetAllCategories(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            //IsValid = consultando os parametros de [Required]
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM);
                if (result != null)
                    return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await
                    _categoryService.GetAllCategories(), "CategoryId", "Name");
            }
            return View(productVM);
        }
    }
}