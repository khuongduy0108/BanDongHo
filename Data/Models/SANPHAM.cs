
namespace BanDongHoSolution.Data.Models
{
    public class SANPHAM
    {
        public int MASP { get; set; }
        public int MATH { get; set; }
        public string? MALOAISP { get; set; }
        public string? TENSP { get; set; }
        public string? HINHLON { get; set; }
        public string? HINHNHO { get; set; }
        public string? MOTA { get; set; }
        public string? DANHGIA { get; set; }
        public int SOLUONG { get; set; }
        public decimal? DONGIA { get; set; }
        public THUONGHIEU? THUONGHIEU { get; set; }
        public LOAISANPHAM? LOAISANPHAM { get; set; }
        public List<CHITIETKM>? CHITIETKMs { get; set; }
        public List<CHITIETDONHANG>? CHITIETDONHANGs { get; set; }
    }
}
