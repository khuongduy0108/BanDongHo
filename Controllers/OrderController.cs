using BanDongHoSolution.Data.Dtos.Cart;
using BanDongHoSolution.Services.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanDongHoSolution.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _orderService.GetAllOrder();
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> CheckOrder()
        {
            var response = await _orderService.CheckOrder(int.Parse(HttpContext.Session.GetString("MaKH")!));
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderById(id);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> DuyetHuyDon(int id, string trangThai)
        {
            var response = await _orderService.DuyetDon(id, trangThai);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            return Json(response._Message);
        }
    }
}
