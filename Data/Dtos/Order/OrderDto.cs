namespace BanDongHoSolution.Data.Dtos.Order
{
    public class OrderDto
    {
        public int MADH { get; set; }
        public string? TenKH { get; set; }
        public int MAKH { get; set; }
        public string? TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public string? TRANGTHAI { get; set; }
        public string? DIACHIGIAO { get; set; }
        public string? SDT { get; set; }
        public DateTime? NGAYDAT { get; set; }
        public DateTime? NGAYGIAO { get; set; }
        public string? MOTA { get; set; }
        public decimal? TONGTIEN { get; set; }
    }
}
