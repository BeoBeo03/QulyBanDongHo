using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;
using BanDongHo.Models.State_Pattern;

namespace BanDongHo.Controllers
{
    public class QlyNhapHangController : Controller
    {
        
        // GET: QlyNhapHang
        public ActionResult DanhSachDonHang(string timkiem)
        {
            ViewBag.TuKhoa = timkiem;
            
            if (timkiem != null)
            {
                List<DonHang> listKQ = DongHoDatabase.Instance.DonHang.Where(n => n.IDDonHang.ToString().Contains(timkiem) || n.TenKH.ToString().Contains(timkiem)).ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(DongHoDatabase.Instance.DonHang.Where(n => n.TinhTrang != "Chưa duyệt"));
                }
                return View(listKQ);
            }
            return View(DongHoDatabase.Instance.DonHang.Where(n => n.TinhTrang != "Chưa duyệt"));
        }
        public ActionResult DanhSachChuaDuyet(string timkiem)
        {
            
            ViewBag.TuKhoa = timkiem;
            if (timkiem != null)
            {
                List<DonHang> listKQ = DongHoDatabase.Instance.DonHang.Where(n => n.IDDonHang.ToString().Contains(timkiem) && n.TinhTrang == "Chưa duyệt" || n.Customer.TenKH.ToString().Contains(timkiem) && n.TinhTrang == "Chưa duyệt").ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(DongHoDatabase.Instance.DonHang.Where(n => n.TinhTrang == "Chưa duyệt"));
                }
                return View(listKQ.Where(n => n.TinhTrang == "Chưa duyệt"));
            }
            return View(DongHoDatabase.Instance.DonHang.Where(n => n.TinhTrang == "Chưa duyệt"));
        }
        public ActionResult DuyetDonHang(int madh)
        {
            
            DonHang dh = DongHoDatabase.Instance.DonHang.Where(n => n.IDDonHang == madh).SingleOrDefault();

            if (dh != null)
            {
                // Áp dụng hành vi duyệt đơn hàng
                Order order = new Order(new PendingState());
                order.ApproveOrder(dh);

                // Lưu thay đổi vào cơ sở dữ liệu
                DongHoDatabase.Instance.SaveChanges();
            }
            return RedirectToAction("DanhSachChuaDuyet", "QlyNhapHang");
        }
        public ActionResult HuyDonHang(int madh)
        {
            
            List<CTDonHang> cthd = DongHoDatabase.Instance.CTDonHang.Where(n => n.IDDonHang == madh).ToList();
            DonHang dh = DongHoDatabase.Instance.DonHang.Where(n => n.IDDonHang == madh).SingleOrDefault();
            
            dh.TinhTrang = "Đã hủy";
            foreach (var item in cthd)
            {
                DongHoDatabase.Instance.CTDonHang.Remove(item);
                DongHoDatabase.Instance.SaveChanges();
            }
            DongHoDatabase.Instance.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            DongHoDatabase.Instance.SaveChanges();
            return RedirectToAction("DanhSachDonHang", "QlyNhapHang");
        }

        public ActionResult ChiTietDH(int madh, string timkiem)
        {
            
            ViewBag.TuKhoa = timkiem;
            if (timkiem != null)
            {
                List<CTDonHang> listKQ = DongHoDatabase.Instance.CTDonHang.Where(n => n.IDDonHang.ToString().Contains(timkiem)).ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(DongHoDatabase.Instance.CTDonHang.Where(n => n.IDDonHang == madh));

                }
                return View(listKQ.OrderByDescending(n => n.IDDonHang == madh));

            }
            return View(DongHoDatabase.Instance.CTDonHang.Where(n => n.IDDonHang == madh));
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
                var orderHistory = DongHoDatabase.Instance.DonHang.Where(o => o.IDUser == user.IDUser &&
                           (trangThai == "Tất cả" || o.TinhTrang == trangThai)).ToList();

                return View(orderHistory);
            }
        }

    }

}