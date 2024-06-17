using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class AdminProductController : Controller

    {
        // GET: AdminProduct
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] != null)
            {
                var products = db.Product;
                return View(products.ToList());
            }

            return RedirectToAction("Index", "HomePage");
        }
        public ActionResult Create()
        {
            ViewBag.IDPL = new SelectList(db.Category, "IDPhanloai", "TenPhanLoai");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSanpham,IDPhanloai,TenSP,TongSoLuong,GiaBanDau,GiaSP,AnhMinhHoa,AnhMinhHoa1,AnhMoTa1,AnhMoTa2,AnhMoTa3," +
            "AnhMoTa6,MoTaSP,LoaiMay,DuongKinh,Mau,ChatLieuDay,DoChiuNuoc,DoDay,Size")] Product sanPham,
           HttpPostedFileBase ImageFile1, HttpPostedFileBase ImageFile2, HttpPostedFileBase ImageFile3, HttpPostedFileBase ImageFile4
           , HttpPostedFileBase ImageFile5, HttpPostedFileBase ImageFile8)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile1 != null && ImageFile2 != null && ImageFile3 != null && ImageFile4 != null && ImageFile5 != null  && ImageFile8 != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName1 = Path.GetFileName(ImageFile1.FileName);
                    var path1 = Path.Combine(Server.MapPath("/Image"), fileName1);
                    ImageFile1.SaveAs(path1);

                    // Upload file 2
                    var fileName2 = Path.GetFileName(ImageFile2.FileName);
                    var path2 = Path.Combine(Server.MapPath("/Image"), fileName2);

                    ImageFile2.SaveAs(path2);
                    // Upload file 3
                    var fileName3 = Path.GetFileName(ImageFile3.FileName);
                    var path3 = Path.Combine(Server.MapPath("/Image"), fileName3);

                    ImageFile3.SaveAs(path3);
                    // Upload file 4
                    var fileName4 = Path.GetFileName(ImageFile4.FileName);
                    var path4 = Path.Combine(Server.MapPath("/Image"), fileName4);

                    ImageFile4.SaveAs(path4);
                    // Upload file 5
                    var fileName5 = Path.GetFileName(ImageFile5.FileName);
                    var path5 = Path.Combine(Server.MapPath("/Image"), fileName5);

                    ImageFile5.SaveAs(path5);
                   
                    // Upload file 8
                    var fileName8 = Path.GetFileName(ImageFile8.FileName);
                    var path8 = Path.Combine(Server.MapPath("/Image"), fileName8);

                    ImageFile8.SaveAs(path8);


                }
                TempData["thongbao"] = "Thêm sản phẩm thành công";
                db.Product.Add(sanPham);

                db.SaveChanges();
                return RedirectToAction("Index", "AdminProduct");
            }

            ViewBag.IDPL = new SelectList(db.Category, "IDPhanloai", "TenPhanLoai", sanPham.IDPhanloai);
            return View(sanPham);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product sanPham = db.Product.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPL = new SelectList(db.Category, "IDPhanloai", "TenPhanLoai");
            return View(sanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "IDSanpham,IDPhanloai,TenSP,TongSoLuong,GiaBanDau,GiaSP,AnhMinhHoa,AnhMinhHoa1,AnhMoTa1,AnhMoTa2,AnhMoTa3," +
            "AnhMoTa6,MoTaSP,LoaiMay,DuongKinh,Mau,ChatLieuDay,DoChiuNuoc,DoDay,Size")] Product sanPham
            , HttpPostedFileBase ImageFile1, HttpPostedFileBase ImageFile2, HttpPostedFileBase ImageFile3, HttpPostedFileBase ImageFile4
           , HttpPostedFileBase ImageFile5,  HttpPostedFileBase ImageFile8)
        {
            if (ModelState.IsValid)
            {
                Product sanPhamToUpdate = db.Product.Find(id);
                /*sanPham.IDPhanloai = sanPham.IDPhanloai;*/
                if(sanPhamToUpdate != null)
                {
                    sanPhamToUpdate.IDPhanloai = sanPham.IDPhanloai;
                    sanPhamToUpdate.TenSP = sanPham.TenSP;
                    sanPhamToUpdate.TongSoLuong = sanPham.TongSoLuong;
                    sanPhamToUpdate.GiaBanDau = sanPham.GiaBanDau;
                    sanPhamToUpdate.GiaSP = sanPham.GiaSP;
                    sanPhamToUpdate.AnhMinhHoa = sanPham.AnhMinhHoa;
                    sanPhamToUpdate.AnhMinhHoa1 = sanPham.AnhMinhHoa1;
                    sanPhamToUpdate.AnhMoTa1 = sanPham.AnhMoTa1;
                    sanPhamToUpdate.AnhMoTa2 = sanPham.AnhMoTa2;
                    sanPhamToUpdate.AnhMoTa3 = sanPham.AnhMoTa3;

                    sanPhamToUpdate.AnhMoTa6 = sanPham.AnhMoTa6;
                    sanPhamToUpdate.MoTaSP = sanPham.MoTaSP;
                    sanPhamToUpdate.LoaiMay = sanPham.LoaiMay;
                    sanPhamToUpdate.DuongKinh = sanPham.DuongKinh;
                    sanPhamToUpdate.Mau = sanPham.Mau;
                    sanPhamToUpdate.ChatLieuDay = sanPham.ChatLieuDay;
                    sanPhamToUpdate.DoChiuNuoc = sanPham.DoChiuNuoc;
                    sanPhamToUpdate.DoDay = sanPham.DoDay;
                    sanPhamToUpdate.Size = sanPham.Size;
                    if (ImageFile1 != null && ImageFile2 != null && ImageFile3 != null && ImageFile4 != null && ImageFile5 != null && ImageFile8 != null)
                    {
                        //Lấy tên file của hình được up lên
                        var fileName1 = Path.GetFileName(ImageFile1.FileName);
                        var path1 = Path.Combine(Server.MapPath("/Image"), fileName1);
                        ImageFile1.SaveAs(path1);

                        // Upload file 2
                        var fileName2 = Path.GetFileName(ImageFile2.FileName);
                        var path2 = Path.Combine(Server.MapPath("/Image"), fileName2);

                        ImageFile2.SaveAs(path2);
                        // Upload file 3
                        var fileName3 = Path.GetFileName(ImageFile3.FileName);
                        var path3 = Path.Combine(Server.MapPath("/Image"), fileName3);

                        ImageFile3.SaveAs(path3);
                        // Upload file 4
                        var fileName4 = Path.GetFileName(ImageFile4.FileName);
                        var path4 = Path.Combine(Server.MapPath("/Image"), fileName4);

                        ImageFile4.SaveAs(path4);
                        // Upload file 5
                        var fileName5 = Path.GetFileName(ImageFile5.FileName);
                        var path5 = Path.Combine(Server.MapPath("/Image"), fileName5);

                        ImageFile5.SaveAs(path5);

                        // Upload file 8
                        var fileName8 = Path.GetFileName(ImageFile8.FileName);
                        var path8 = Path.Combine(Server.MapPath("/Image"), fileName8);

                        ImageFile8.SaveAs(path8);
                        /*//Save vào Images Folder
                        ImagePro.SaveAs(path);
                        db.SaveChanges();*/
                    }

                    db.SaveChanges();
                    TempData["thongbao"] = "Sửa sản phẩm thành công";
                    return RedirectToAction("Index");
                }
                TempData["thongbao"] = "Sửa sản phẩm thành công";
                db.SaveChanges();
            }
            db.SaveChanges();
            ViewBag.IDPL = new SelectList(db.Category, "IDPhanloai", "TenPhanLoai", sanPham.IDPhanloai);
            return View(sanPham);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product sanPham = db.Product.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product sanPham = db.Product.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            
            db.Product.Remove(sanPham);

            db.SaveChanges();
            TempData["thongbao"] = "Xóa sản phẩm thành công";
            return RedirectToAction("Index", "AdminProduct");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product sanPham = db.Product.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}