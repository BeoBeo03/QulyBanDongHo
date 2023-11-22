using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public ActionResult Index(int id)
        {
            var product = db.Product.Find(id);
            return View(product);
        }
        public ActionResult SanPhamTheoPhanLoai(string phanloai)
        {
            // Thực hiện truy vấn SQL để lấy sản phẩm theo phân loại
            List<Product> products = db.Product.Where(p => p.Category.TenPhanLoai == phanloai).ToList();

            // Truyền danh sách sản phẩm đã lọc vào view
            ViewBag.CategoryName = db.Category.FirstOrDefault().TenPhanLoai;
            return View(products);
            
        }
    }
}