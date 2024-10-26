using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShop.Web.Models;

namespace VShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro durante o login: {ex.Message}");
                return View("Error");
            }
        }

        public IActionResult Logout()
        {
            try
            {
                return SignOut("Cookies", "oidc");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro durante o logout: {ex.Message}");
                return View("Error");
            }
        }
    }
}
