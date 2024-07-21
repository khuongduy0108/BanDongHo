
namespace BanDongHoSolution.Data.Models
{
    public class KHUYENMAI
    {
        public string? MAKM { get; set; }
        public string? TENKM { get; set;}
        public DateTime? NGAYBD { get; set; }
        public DateTime? NGAYKT { get; set; }
        public List<CHITIETKM>? CHITIETKMs { get; set; }
    }
}
