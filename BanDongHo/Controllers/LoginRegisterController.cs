using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class LoginRegisterController : Controller
    {
        // GET: LoginRegister
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public ActionResult Login()
        {
            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer kh)
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
                        return RedirectToAction("Index", "Admin");

                    }
                    else
                    {
                        var khachhang = db.Customer.FirstOrDefault(k => k.Email == kh.Email && k.MatKhau == kh.MatKhau);
                        if (khachhang != null)
                        {
                            ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                            Session["TaiKhoan"] = khachhang;
                            return RedirectToAction("Index", "HomePage");
                        }
                        else
                        {
                            ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                            ModelState.AddModelError("", "Sai mật khẩu!");

                        }
                        return RedirectToAction("Login", "LoginRegister");
                    }
                }
            }
            return RedirectToAction("Index", "HomePage");


        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cust,string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(cust.MatKhau))
                    ModelState.AddModelError(string.Empty, "MK không được để trống");
                if (string.IsNullOrEmpty(cust.MatKhau))
                    ModelState.AddModelError(string.Empty, "MK không được để trống");
                if (string.IsNullOrEmpty(cust.GioiTinh))
                    ModelState.AddModelError(string.Empty, "GT không được để trống");
                if (string.IsNullOrEmpty(cust.TenKH))
                    ModelState.AddModelError(string.Empty, "TenKH không được để trống");

                //Quy định mk phải tối thiểu và tối đa 10 số

                //Quy định mk và nhập lại mk phải giống nhau    
                
                //Quy dịnh sdt phải tối thiểu va tối đa 10 số


                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhậpnày hay chưa
                var khachhang = db.Customer.FirstOrDefault(k => k.Email == cust.Email);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng ký");


                if (ModelState.IsValid)
                {
                    //cust.phanquyen=2;
                    db.Customer.Add(cust);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Đã có người đăng ký";
                    return View();
                }
            }
            return RedirectToAction("Login");
        }
        public ActionResult LogOut()
        {
            Session["TaiKhoan"] = null;
            return Redirect("/");
        }

    }
}