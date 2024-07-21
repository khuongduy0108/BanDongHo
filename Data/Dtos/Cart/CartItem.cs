using BanDongHoSolution.Data.Models;

namespace BanDongHoSolution.Data.Dtos.Cart
{
    public class CartItem
    {
        public SANPHAM? SANPHAM { get; set; }
        public int Quantity { get; set; }
    }
}
