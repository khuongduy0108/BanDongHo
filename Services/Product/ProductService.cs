using BanDongHoSolution.Data.Contexts;
using BanDongHoSolution.Data.Dtos.Product;
using BanDongHoSolution.Data.Models;
using BanDongHoSolution.Response;
using Microsoft.EntityFrameworkCore;

namespace BanDongHoSolution.Services.Product
{
    public interface IProductService
    {
        Task<BaseResponse<SANPHAM>> AddProduct(ProductDto request);
        Task<BaseResponse<SANPHAM>> UpdateProduct(ProductDto request);
        Task<BaseResponse<SANPHAM>> DeleteProduct(int idProduct);
        Task<List<ProductDto>> GetAllProduct();
        Task<List<THUONGHIEU>> GetListThuongHieu();
        Task<List<LOAISANPHAM>> GetListLoaiSP();
        Task<List<ProductDto>> SearchProductByName(string keyword);
    }
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string _imageContentFolder;
        public ProductService(AppDbContext appContext, IWebHostEnvironment webHostEnvironment)
        {
            _imageContentFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            _appContext = appContext;
            this.webHostEnvironment = webHostEnvironment;   
        }
        public async Task<BaseResponse<SANPHAM>> AddProduct(ProductDto request)
        {
            var success = false;
            var message = "";
            var data = new SANPHAM();
            var sp = await _appContext.SANPHAMs.FirstOrDefaultAsync(x=>x.TENSP == request.TenSP);
            if (sp != null)
            {
                message = "Sản phẩm đã tồn tại";
                return new BaseResponse<SANPHAM>(success, message, null!);
            }
            data.TENSP = request.TenSP;
            data.MOTA = request.MoTa;
            data.MALOAISP = request.MaLoaiSP;
            data.DANHGIA = request.DanhGia;
            data.DONGIA = request.DonGia;
            data.HINHLON = UploadedFile(request.HinhLonFile!);
            data.HINHNHO = UploadedFile(request.HinhNhoFile!);
            data.SOLUONG = request.SoLuong;
            data.MATH = request.MaTH;
            await _appContext.AddAsync(data);
            await _appContext.SaveChangesAsync();
            success = true;
            message = "Thêm sản phẩm thành công";
            return new BaseResponse<SANPHAM>(success, message, data);
        }
        public async Task<BaseResponse<SANPHAM>> UpdateProduct(ProductDto request)
        {
            var success = false;
            var message = "";
            var sp = await _appContext.SANPHAMs.FirstOrDefaultAsync(x => x.MASP == request.MaSP);
            if (sp == null)
            {
                message = "Sản phẩm không tồn tại";
                return new BaseResponse<SANPHAM>(success, message, null!);
            }
            sp.TENSP = request.TenSP;
            sp.MOTA = request.MoTa;
            sp.MALOAISP = request.MaLoaiSP;
            sp.DANHGIA = request.DanhGia;
            sp.DONGIA = request.DonGia;
            if (request.HinhLonFile != null)
            {
                var n = sp.HINHLON!.Remove(0, 8);
                await DeleteImage(n);
                sp.HINHLON = UploadedFile(request.HinhLonFile!);
            }
            if (request.HinhNhoFile != null)
            {
                var n = sp.HINHNHO!.Remove(0, 8);
                await DeleteImage(n);
                sp.HINHNHO = UploadedFile(request.HinhNhoFile!);
            }
            sp.SOLUONG = request.SoLuong;
            sp.MATH = request.MaTH;
            await _appContext.SaveChangesAsync();
            success = true;
            message = "Chỉnh sửa thông tin sản phẩm thành công";
            return new BaseResponse<SANPHAM>(success, message, sp);
        }
        public async Task<BaseResponse<SANPHAM>> DeleteProduct(int idProduct)
        {
            var success = false;
            var message = "";
            var sp = await _appContext.SANPHAMs.FirstOrDefaultAsync(x => x.MASP == idProduct);
            if (sp != null)
            {
                success = true;
                message = "Xóa sản phẩm thành công";
                var n = sp.HINHLON!.Remove(0, 8);
                await DeleteImage(n);
                var m = sp.HINHNHO!.Remove(0, 8);
                await DeleteImage(m);
                _appContext.Remove(sp);
                await _appContext.SaveChangesAsync();
                return new BaseResponse<SANPHAM>(success, message, null!);
            }
            message = "Sản phẩm không tồn tại";
            return new BaseResponse<SANPHAM>(success, message, null!);
        }
        public async Task<List<ProductDto>> GetAllProduct()
        {
            var query = from a in _appContext.SANPHAMs
                        join b in _appContext.LOAISANPHAMs
                        on a.MALOAISP equals b.MALOAISP
                        join c in _appContext.THUONGHIEUs
                        on a.MATH equals c.MATH
                        select new { a, b, c };
            if (query.Count() > 0)
            {
                var data = await query.Select(x => new ProductDto()
                {
                    MaSP = x.a.MASP, 
                    MaTH = x.a.MATH,
                    MaLoaiSP = x.a.MALOAISP,
                    TenSP = x.a.TENSP,
                    ThuongHieu = x.c.TENTH,
                    LoaiSP = x.b.TENLOAISP,
                    HinhLon = x.a.HINHLON,
                    HinhNho = x.a.HINHNHO,
                    MoTa = x.a.MOTA,
                    DanhGia = x.a.DANHGIA,
                    SoLuong = x.a.SOLUONG,
                    DonGia = x.a.DONGIA
                }).ToListAsync();
                return data;
            }
            return null!;
        }
        public async Task<List<THUONGHIEU>> GetListThuongHieu()
        {
            var data = await _appContext.THUONGHIEUs.ToListAsync();
            return data;
        }
        public async Task<List<LOAISANPHAM>> GetListLoaiSP()
        {
            var data = await _appContext.LOAISANPHAMs.ToListAsync();
            return data;
        }
        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null!;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return "/images/"+uniqueFileName!;
        }
        public async Task DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_imageContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
        public async Task<List<ProductDto>> SearchProductByName(string keyword)
        {
            var query = from a in _appContext.SANPHAMs
                        join b in _appContext.LOAISANPHAMs
                        on a.MALOAISP equals b.MALOAISP
                        join c in _appContext.THUONGHIEUs
                        on a.MATH equals c.MATH
                        where a.TENSP.Contains(keyword) // thêm điều kiện tìm kiếm theo tên sản phẩm
                        select new { a, b, c };
            if (query.Count() > 0)
            {
                var data = await query.Select(x => new ProductDto()
                {
                    MaSP = x.a.MASP,
                    MaTH = x.a.MATH,
                    MaLoaiSP = x.a.MALOAISP,
                    TenSP = x.a.TENSP,
                    ThuongHieu = x.c.TENTH,
                    LoaiSP = x.b.TENLOAISP,
                    HinhLon = x.a.HINHLON,
                    HinhNho = x.a.HINHNHO,
                    MoTa = x.a.MOTA,
                    DanhGia = x.a.DANHGIA,
                    SoLuong = x.a.SOLUONG,
                    DonGia = x.a.DONGIA
                }).ToListAsync();
                return data;
            }
            return null!;
        }
        

    }
}
