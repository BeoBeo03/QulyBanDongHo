using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();

        [HttpPost]
        public ActionResult KQTimKiem(FormCollection f)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<Product> listKQ = db.Product.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();


            ViewBag.ThongBao1 = "Đã tìm thấy " + listKQ.Count + " kết quả!";
            return View(listKQ);
        }
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<Product> listKQ = db.Product.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();

            if (listKQ.Count == 0)
            {
                ViewBag.ThongBao1 = "Không tìm thấy sản phẩm nào phù hợp.";
                ViewBag.ThongBao2 = "Thử xem một số mẫu giày khác.";
                return View(listKQ);
            }
            ViewBag.ThongBao1 = "Đã tìm thấy " + listKQ.Count + " kết quả!";
            return View(listKQ);
        }
    }
}