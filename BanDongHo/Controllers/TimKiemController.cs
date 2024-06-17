    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;
using BanDongHo.Models.Strategy_Pattern;


namespace BanDongHo.Controllers 
{ 
    public class TimKiemController : Controller
    {
        
        
        [HttpPost]
        public ActionResult KQTimKiem(FormCollection f)
        {
            ISearchStrategy searchStrategy = new NameSearchStrategy();//

            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<Product> listKQ = searchStrategy.Search(sTuKhoa);

            ViewBag.ThongBao1 = "Đã tìm thấy " + listKQ.Count + " kết quả!";
            return View(listKQ);
        }

        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            ISearchStrategy searchStrategy = new NameSearchStrategy();//chiến lược tìm kiếm được lưu ở searchStrategy
            ViewBag.TuKhoa = sTuKhoa;
            List<Product> listKQ = searchStrategy.Search(sTuKhoa);//

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