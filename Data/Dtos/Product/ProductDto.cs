namespace BanDongHoSolution.Data.Dtos.Product
{
    public class ProductDto
    {
        public int MaSP { get; set; }
        public int MaTH { get; set; }
        public string? ThuongHieu { get; set; }
        public string? MaLoaiSP { get; set; }
        public string? LoaiSP { get; set; }
        public string? TenSP { get; set; }
        public string? HinhLon { get; set; }
        public string? HinhNho { get; set; }
        public IFormFile? HinhLonFile { get; set; }
        public IFormFile? HinhNhoFile { get; set; }
        public string? MoTa { get; set; }
        public string? DanhGia { get; set; }
        public int SoLuong { get; set; }
        public decimal? DonGia { get; set; }
    }
}
