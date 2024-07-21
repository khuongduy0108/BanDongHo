using Azure;
using BanDongHoSolution.Data.Dtos.Account;
using BanDongHoSolution.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BanDongHoSolution.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = "LK00001")]
        public IActionResult AdminOnly()
        {
            return View();
        }

        [Authorize(Roles = "LK00002,LK00001")]
        public IActionResult UserAndAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _accountService.Login(loginDto.UserName!, loginDto.Password!);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            if (response._success == true)
            {

                HttpContext.Session.SetString("UserName", loginDto.UserName!);
                HttpContext.Session.SetString("LoaiTK", response._Data.TAIKHOAN!.MALOAITK!);
                HttpContext.Session.SetString("TenKH", response._Data.TENKH!);
                HttpContext.Session.SetString("Email", response._Data.EMAIL!);
                HttpContext.Session.SetString("SDT", response._Data.SDT!);
                HttpContext.Session.SetString("DiaChi", response._Data.DIACHI!);
                HttpContext.Session.SetString("MaKH", response._Data.MAKH.ToString()!);
                return RedirectToAction("Index", "Home");
            }
            return View(loginDto);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(registerDto.MatKhau != registerDto.XacNhanMatKhau)
            {
                TempData["Message"] = "Mật khẩu xác nhận không đúng";

                TempData["Success"] = false;
                return View(registerDto);
            }
            var response = await _accountService.Register(registerDto);
            TempData["Message"] = response._Message;
            TempData["Success"] = response._success;
            if (response._success == true)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(registerDto);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
