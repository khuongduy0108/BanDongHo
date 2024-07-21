
namespace BanDongHoSolution.Data.Models
{
    public class DONHANG
    {
        public int MADH { get; set; }
        public int MAKH { get; set; }
        public string? TRANGTHAI { get; set; }
        public string? DIACHIGIAO { get; set; }
        public string? SDT { get; set; }
        public DateTime? NGAYDAT { get; set; }
        public DateTime? NGAYGIAO { get; set; }
        public string? MOTA { get; set; }
        public decimal? TONGTIEN { get; set; }
        public KHACHHANG? KHACHHANG { get; set; }
        public List<CHITIETDONHANG>? CHITIETDONHANGs { get; set; }
    }
}
