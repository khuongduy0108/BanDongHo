using BanDongHoSolution.Common;
using BanDongHoSolution.Data.Contexts;
using BanDongHoSolution.Data.Dtos.Account;
using BanDongHoSolution.Data.Models;
using BanDongHoSolution.Response;
using Microsoft.EntityFrameworkCore;

namespace BanDongHoSolution.Services.Account
{
    public interface IAccountService
    {
        Task<BaseResponse<KHACHHANG>> Login(string username, string password);
        Task<BaseResponse<TAIKHOAN>> Register(RegisterDto request);
    }
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _appContext;
        public AccountService(AppDbContext appContext)
        {
            _appContext = appContext;
        }
        public async Task<BaseResponse<KHACHHANG>> Login(string username, string password)
        {
            var success = false;
            var message = "";
            var kh = new KHACHHANG();
            var user = await _appContext.TAIKHOANs.FirstOrDefaultAsync(x => x.TENDN == username && x.MATKHAU == CreateMD5.MD5Hash(password));
            if (user != null)
            {
                success = true;
                message = "Đăng nhập thành công";
                kh = await _appContext.KHACHHANGs.Include(x=>x.TAIKHOAN).FirstOrDefaultAsync(x => x.MATK == user.MATK);
            }
            else
            {
                message = "Đăng nhập không thành công";
            }
            return new BaseResponse<KHACHHANG>(success, message, kh!);
        }
        public async Task<BaseResponse<TAIKHOAN>> Register(RegisterDto request)
        {
            var success = false;
            var message = "";
            var data = new TAIKHOAN();
            var user = await _appContext.TAIKHOANs.FirstOrDefaultAsync(x => x.TENDN == request.TenDN);
            if(user != null)
            {
                message = "Tên đăng nhập đã tồn tại";
                return new BaseResponse<TAIKHOAN>(success, message, null!);
            }
            data.TENDN = request.TenDN;
            data.NGAYDANGKY = DateTime.Now;
            data.MATKHAU = CreateMD5.MD5Hash(request.MatKhau!);
            data.MALOAITK = "LK00002";
            data.TRANGTHAI = true;
            await _appContext.AddAsync(data);
            await _appContext.SaveChangesAsync();

            var kh = new KHACHHANG();
            kh.MATK = data!.MATK;
            kh.SDT = request.SDT;
            kh.DIACHI = request.DiaChi;
            kh.EMAIL= request.Email;
            kh.GIOITINH = request.GioiTinh;
            kh.TENKH = request.TenKH;
            await _appContext.AddAsync(kh);
            await _appContext.SaveChangesAsync();
            success = true;
            message = "Đăng kí thành công";
            return new BaseResponse<TAIKHOAN>(success, message, data);
        }
    }
}
