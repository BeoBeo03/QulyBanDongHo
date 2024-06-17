using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanDongHo.Models.Factory_Pattern
{
    public class User : Controller, ILogin<Customer>
    {
        // GET: User
        [HttpGet]
        public bool DangNhap(string taikhoan)
        {
            return taikhoan != null;
        }
        [HttpPost]
        public bool DangNhap(Customer kh, ref object taikhoan)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Email  không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    if (kh.Email == "admin@gmail.com" && kh.MatKhau == "123")
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        Session["TaiKhoanAdmin"] = "Admin";
                        taikhoan = kh;
                        return true;

                    } 
                    else
                    {
                        var khachhang = DongHoDatabase.Instance.Customer.FirstOrDefault(k => k.Email == kh.Email && k.MatKhau == kh.MatKhau);
                        if (khachhang != null)
                        {
                            ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                            taikhoan = khachhang;
                            return true;
                        }
                        else
                        {
                            ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                            ModelState.AddModelError("", "Sai mật khẩu!");
                            return false;
                        }
                       
                    }
                    
                }
                
            }
            return false;
        }
       
    }
}