namespace BanDongHoSolution.Data.Dtos.Account
{
    public class RegisterDto
    {
        public string? MaLoaiTK { get; set; }
        public string? TenDN { get; set; }
        public string? MatKhau { get; set; }
        public string? XacNhanMatKhau { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public int TrangThai { get; set; }
        public string? TenKH { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public string? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
    }
}
