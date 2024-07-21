namespace BanDongHoSolution.Data.Dtos.Cart
{
    public class DetailOrder
    {
        public int MaKH { get; set; }
        public string? DiaChiGiao { get; set; }
        public string? SDT { get; set; }
        public string? MoTa { get; set; }
        public decimal? TongTien { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
