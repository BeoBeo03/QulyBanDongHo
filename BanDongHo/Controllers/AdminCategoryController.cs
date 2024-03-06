using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public ActionResult DanhSachPhanLoai()
        {
            if (Session["TaiKhoanAdmin"] != null)
            {
                var category = db.Category;
                return View(category.ToList());
            }
            return RedirectToAction("Index", "HomePage");
        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhanloai,TenPhanLoai")] Category phanLoai)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(phanLoai);
                db.SaveChanges();
                TempData["thongbao"] = "Thêm mới màu thành công!";
            }
            else
            {
                TempData["thongbao"] = "Thêm mới màu thất bại";
            }
            return View(phanLoai);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category phanLoai = db.Category.Find(id);
            if (phanLoai == null)
            {
                return HttpNotFound();
            }
            
            return View(phanLoai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "IDPhanloai,TenPhanLoai")] Category phanLoai)
        {
            if (ModelState.IsValid)
            {
                Category phanLoaiToUpdate = db.Category.Find(id);
                if(phanLoaiToUpdate != null)
                {
                    phanLoaiToUpdate.IDPhanloai = phanLoai.IDPhanloai;
                    phanLoaiToUpdate.TenPhanLoai = phanLoai.TenPhanLoai;
                    db.SaveChanges();
                    TempData["thongbao"] = "Chỉnh sửa thành công!";
                }
                db.SaveChanges();
            }
            else 
            {
                TempData["thongbao"] = "Chỉnh sửa thất bại!";
            }
            
            return View(phanLoai);

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category phanLoai = db.Category.Find(id);

            if (phanLoai == null)
            {
                return HttpNotFound();
            }

            return View(phanLoai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category phanLoai = db.Category.Find(id);

            // Kiểm tra xem phân loại có đang được sử dụng trong sản phẩm hay không
            bool isUsedInProduct = db.Product.Any(s => s.IDPhanloai == id);

            if (isUsedInProduct)
            {
                TempData["thongbao"] = "Không thể xóa phân loại vì đang được sử dụng trong sản phẩm.";
                return RedirectToAction("DanhSachPhanLoai");
            }

            db.Category.Remove(phanLoai);
            db.SaveChanges();

            TempData["thongbao"] = "Xóa phân loại thành công.";
            return View(phanLoai);
        }
    }
}