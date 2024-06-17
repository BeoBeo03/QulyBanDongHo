using System;
using System.Collections.Generic;
using System.Linq;
using BanDongHo.Models.Factory_Pattern;
using BanDongHo.Models.Decorator_Pattern;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class LoginRegisterController : Controller
    {
        // GET: LoginRegister
        LoginFactory<Customer> dangNhapFactory;//tạo 1 phiên bản cụ thể cho ilogin
        ILogin<Customer> user;
        void CreateLogin()
        {
            dangNhapFactory = new LoginUser();
            user = dangNhapFactory.CreateLogin();
        }
        [HttpGet]
        public ActionResult Login()
        {
           
            String taiKhoan = Session["TaiKhoan"] as String;
            CreateLogin();

            if (user.DangNhap(taiKhoan))
            {
                return RedirectToAction("Index", "HomePage");
            }

            else
                return View();
        }
        object taikhoan;
        [HttpPost]
        public ActionResult Login(Customer x)
        {
            CreateLogin();
            if (x.Email == "admin@gmail.com" && x.MatKhau == "123")
            {
                // Thiết lập session cho tài khoản admin
                Session["TaiKhoanAdmin"] = "Admin";

                // Chuyển hướng đến trang chính sau khi đăng nhập thành công
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                if (user.DangNhap(x, ref taikhoan))
                {
                    Session["TaiKhoan"] = taikhoan;
                    return Redirect("/");
                }
                else
                    return View();
            }
            


        }
        [HttpGet]
        public ActionResult Register()
        {

            String taiKhoan = Session["TaiKhoan"] as String;
            if (taiKhoan != null)
                return RedirectToAction("Index", "HomePage");
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cust)
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

                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhậpnày hay chưa
                var khachhang = DongHoDatabase.Instance.Customer.FirstOrDefault(k => k.Email == cust.Email);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng ký");


                if (ModelState.IsValid)
                {

                    Customer kh = new Customer();
                    AbstractCustomer khachHang = new ConcreteCustomer();
                    khachHang.MakeCustomer();
                    // Họ và tên
                    khachHang = new TenKHDecorator(khachHang, cust.TenKH, kh);
                    kh = khachHang.MakeCustomer();
                    // Email
                    khachHang = new EmailDecorator(khachHang, cust.Email, kh);
                    kh = khachHang.MakeCustomer();
                    //Giới tính
                    khachHang = new GioiTinhDecorator(khachHang, cust.GioiTinh, kh);
                    kh = khachHang.MakeCustomer();
                    // SDT
                    khachHang = new SDTDecorator(khachHang, (int)cust.SDT, kh);
                    kh = khachHang.MakeCustomer();
                    //Mật khẩu
                    khachHang = new MatKhauDecorator(khachHang, cust.MatKhau, kh);
                    kh = khachHang.MakeCustomer();

                    //cust.phanquyen=2;
                    DongHoDatabase.Instance.Customer.Add(kh);
                    DongHoDatabase.Instance.SaveChanges();
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