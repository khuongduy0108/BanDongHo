using BanDongHoSolution.Data.Contexts;
using BanDongHoSolution.Data.Dtos.Cart;
using BanDongHoSolution.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BanDongHoSolution.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _appContext;
        private readonly IOrderService _orderService;
        public CartController(AppDbContext appContext, IOrderService orderService)
        {
            _appContext = appContext;
            _orderService = orderService;
        }

        public const string CARTKEY = "cart";
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY)!;
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart)!;
            }
            return new List<CartItem>();
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        [Route("/cart", Name = "cart")]
        public IActionResult Index()
        {
            return View(GetCartItems());
        }

        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {
            // Kiểm tra đăng nhập
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var product = _appContext.SANPHAMs
                .Where(p => p.MASP == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SANPHAM!.MASP == productid);
            if (cartitem != null)
            {
                cartitem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem() { Quantity = 1, SANPHAM = product });
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Index));
        }
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SANPHAM!.MASP == productid);
            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            return Ok();
        }
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SANPHAM!.MASP == productid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(DetailOrder request)
        {
            request.CartItems = GetCartItems();
            request.MaKH = int.Parse(HttpContext.Session.GetString("MaKH")!);
            var response = await _orderService.AddOrder(request);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            return RedirectToAction("Index", "Home");
        }
    }
}
