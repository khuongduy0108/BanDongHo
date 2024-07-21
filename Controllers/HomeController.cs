using BanDongHoSolution.Models;
using BanDongHoSolution.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BanDongHoSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        public HomeController(IProductService productService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllProduct();
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Search(string keyword)
        {
            var products = await _productService.SearchProductByName(keyword);
            return View("Index", products);
        }

    }
}