using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class QlyNhapHangController : Controller
    {
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        // GET: QlyNhapHang
        public ActionResult DanhSachDonHang(string timkiem)
        {
            ViewBag.TuKhoa = timkiem;
            
            if (timkiem != null)
            {
                List<DonHang> listKQ = db.DonHang.Where(n => n.IDDonHang.ToString().Contains(timkiem) || n.TenKH.ToString().Contains(timkiem)).ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(db.DonHang.Where(n => n.TinhTrang != "Chưa duyệt"));
                }
                return View(listKQ);
            }
            return View(db.DonHang.Where(n => n.TinhTrang != "Chưa duyệt"));
        }
        public ActionResult DanhSachChuaDuyet(string timkiem)
        {
            
            ViewBag.TuKhoa = timkiem;
            if (timkiem != null)
            {
                List<DonHang> listKQ = db.DonHang.Where(n => n.IDDonHang.ToString().Contains(timkiem) && n.TinhTrang == "Chưa duyệt" || n.Customer.TenKH.ToString().Contains(timkiem) && n.TinhTrang == "Chưa duyệt").ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(db.DonHang.Where(n => n.TinhTrang == "Chưa duyệt"));
                }
                return View(listKQ.Where(n => n.TinhTrang == "Chưa duyệt"));
            }
            return View(db.DonHang.Where(n => n.TinhTrang == "Chưa duyệt"));
        }
        public ActionResult DuyetDonHang(int madh)
        {
            
            DonHang dh = db.DonHang.Where(n => n.IDDonHang == madh).SingleOrDefault();
            
            dh.TinhTrang = "Đã duyệt";
            dh.NgayGiao = DateTime.Now;
            db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachChuaDuyet", "QlyNhapHang");
        }
        public ActionResult HuyDonHang(int madh)
        {
            
            List<CTDonHang> cthd = db.CTDonHang.Where(n => n.IDDonHang == madh).ToList();
            DonHang dh = db.DonHang.Where(n => n.IDDonHang == madh).SingleOrDefault();
            
            dh.TinhTrang = "Đã hủy";
            foreach (var item in cthd)
            {
                db.CTDonHang.Remove(item);
                db.SaveChanges();
            }
            db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachDonHang", "QlyNhapHang");
        }

        public ActionResult ChiTietDH(int madh, string timkiem)
        {
            
            ViewBag.TuKhoa = timkiem;
            if (timkiem != null)
            {
                List<CTDonHang> listKQ = db.CTDonHang.Where(n => n.IDDonHang.ToString().Contains(timkiem)).ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(db.CTDonHang.Where(n => n.IDDonHang == madh));

                }
                return View(listKQ.OrderByDescending(n => n.IDDonHang == madh));

            }
            return View(db.CTDonHang.Where(n => n.IDDonHang == madh));
        }
        public ActionResult TinhTrangDonHang(string trangThai = "Tất cả")
        {



            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            else
            {
                Customer user = Session["TaiKhoan"] as Customer;

                // Truy vấn cơ sở dữ liệu để lấy lịch sử đơn hàng của người dùng
                var orderHistory = db.DonHang.Where(o => o.IDUser == user.IDUser &&
                           (trangThai == "Tất cả" || o.TinhTrang == trangThai)).ToList();

                return View(orderHistory);
            }
        }

    }

}