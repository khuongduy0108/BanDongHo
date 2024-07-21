
namespace BanDongHoSolution.Data.Models
{
    public class CHITIETDONHANG
    {
        public int MADH { get; set; }
        public int MASP { get; set; }
        public int SOLUONG { get; set; }
        public DONHANG? DONHANG { get; set; }
        public SANPHAM? SANPHAM { get; set; }
    }
}
