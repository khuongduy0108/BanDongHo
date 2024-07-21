
namespace BanDongHoSolution.Data.Models
{
    public class TAIKHOAN
    {
        public int MATK { get; set; }
        public string? MALOAITK { get; set; }
        public string? TENDN { get; set; }
        public string? MATKHAU { get; set; }
        public DateTime? NGAYDANGKY { get; set; }
        public bool TRANGTHAI { get; set; }
        public LOAITK? LOAITK { get; set; }
        public List<KHACHHANG>? KHACHHANGs { get; set; }
    }
}
