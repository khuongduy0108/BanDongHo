using BanDongHoSolution.Data.Contexts;
using BanDongHoSolution.Data.Dtos.Cart;
using BanDongHoSolution.Data.Dtos.Order;
using BanDongHoSolution.Data.Models;
using BanDongHoSolution.Response;
using Microsoft.EntityFrameworkCore;

namespace BanDongHoSolution.Services.Order
{
    public interface IOrderService
    {
        Task<List<DONHANG>> GetAllOrder();
        Task<BaseResponse<DONHANG>> AddOrder(DetailOrder request);
        Task<List<OrderDto>> GetOrderById(int id);
        Task<BaseResponse<DONHANG>> DuyetDon(int id, string trangthai);
        Task<List<DONHANG>> CheckOrder(int maKH);
    }
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appContext;
        public OrderService(AppDbContext appContext)
        {
            _appContext = appContext;
        }
        public async Task<List<DONHANG>> GetAllOrder()
        {
            var data = await _appContext.DONHANGs.ToListAsync();
            return data;
        }
        public async Task<BaseResponse<DONHANG>> AddOrder(DetailOrder request)
        {
            var success = false;
            var message = "";
            var donHang = new DONHANG();
            donHang.MAKH = request.MaKH;
            donHang.DIACHIGIAO = request.DiaChiGiao;
            donHang.NGAYDAT = DateTime.Now;
            donHang.NGAYGIAO = null!;
            donHang.TRANGTHAI = "Chờ xử lý";
            donHang.SDT = request.SDT;
            donHang.MOTA = request.MoTa;
            donHang.TONGTIEN= request.TongTien;
            await _appContext.AddAsync(donHang);
            await _appContext.SaveChangesAsync();

            if (request.CartItems != null)
            {
                foreach(var item in request.CartItems)
                {
                    var chiTietDonHang = new CHITIETDONHANG();
                    chiTietDonHang.MADH = donHang.MADH;
                    chiTietDonHang.MASP = item.SANPHAM!.MASP;
                    chiTietDonHang.SOLUONG = item.Quantity;
                    await _appContext.AddAsync(chiTietDonHang);
                    await _appContext.SaveChangesAsync();
                }
                success = true;
                message = "Thêm đơn hàng thành công";
            }
            else
            {
                message = "Thêm đơn hàng không thành công";
            }
            return new BaseResponse<DONHANG>(success, message, null!);
        }
        public async Task<List<OrderDto>> GetOrderById(int id)
        {
            var query = from a in _appContext.DONHANGs.Include(x => x.KHACHHANG)
                       join b in _appContext.CHITIETDONHANGs.Include(x=>x.SANPHAM)
                       on a.MADH equals b.MADH
                       where a.MADH == id
                       select new { a, b };
            if (query != null)
            {
                var data = await query.Select(x=>new OrderDto()
                {
                    TenKH = x.a.KHACHHANG!.TENKH,
                    TenSP = x.b.SANPHAM!.TENSP,
                    SoLuong = x.b.SOLUONG,
                    DonGia = x.b.SANPHAM!.DONGIA
                }).ToListAsync();
                return data;
            }
            return null!;
        }
        public async Task<BaseResponse<DONHANG>> DuyetDon(int id, string trangthai)
        {
            var success = false;
            var message = "";
            var data = await _appContext.DONHANGs.FirstOrDefaultAsync(x => x.MADH == id);
            if (data != null)
            {
                data.TRANGTHAI = trangthai;
                await _appContext.SaveChangesAsync();
                success = true;
                if(trangthai == "Chờ xử lý")
                {
                    message = "Hủy duyệt đơn thành công";
                }
                else
                {
                    message = "Duyệt đơn thành công";
                }
            }
            else
            {
                if (trangthai == "Chờ xử lý")
                {
                    message = "Hủy duyệt đơn không thành công";
                }
                else
                {
                    message = "Duyệt đơn không thành công";
                }
            }
            return new BaseResponse<DONHANG>(success, message, data!);
        }
        public async Task<List<DONHANG>> CheckOrder(int maKH)
        {
            var data = await _appContext.DONHANGs.Where(x=>x.MAKH == maKH).ToListAsync();
            return data;
        }
    }
}
