using BanDongHoSolution.Data.Dtos.Product;
using BanDongHoSolution.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace BanDongHoSolution.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllProduct();
            var response1 = await _productService.GetListThuongHieu();
            ViewBag.ThuongHieu = response1;
            var response2 = await _productService.GetListLoaiSP();
            ViewBag.LoaiSP = response2;
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {

            var response = await _productService.AddProduct(productDto);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            var response = await _productService.UpdateProduct(productDto);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            return RedirectToAction("Index", "Product");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int idProduct)
        {
            var response = await _productService.DeleteProduct(idProduct);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            return Json(response._Message);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
        {
            var products = await _productService.SearchProductByName(keyword);
            ViewBag.Keyword = keyword;
            return View("Index", products);
        }


    }
}
