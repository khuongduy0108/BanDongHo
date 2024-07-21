
namespace BanDongHoSolution.Data.Models
{
    public class KHACHHANG
    {
        public int MAKH { get; set; }
        public int MATK { get; set; }
        public string? TENKH { get; set; }
        public string? EMAIL { get; set; }
        public string? SDT { get; set; }
        public string? GIOITINH { get; set; }
        public string? DIACHI { get; set; }
        public TAIKHOAN? TAIKHOAN { get; set; }
        public List<DONHANG>? DONHANGs { get; set; }
    }
}
