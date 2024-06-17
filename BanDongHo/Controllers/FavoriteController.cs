using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;
using BanDongHo.Models.Command_Pattern;

namespace BanDongHo.Controllers
{
    public class FavoriteController : Controller
    {
        // GET: Favorite
        
        public ActionResult Index()
        {
            // Lấy danh sách sản phẩm đã được yêu thíchA từ cơ sở dữ liệu
            if (Session["TaiKhoan"] != null)
            {
                var userID = ((Customer)Session["TaiKhoan"]).IDUser;
                // Lấy danh sách sản phẩm yêu thích của người dùng và trả về view.
                var danhSachYeuThich = DongHoDatabase.Instance.Favorite.Where(yt => yt.IDUser == userID).ToList();
                return View(danhSachYeuThich);
            }
            else
            {
                return RedirectToAction("Login", "LoginRegister");
            }
        }
        [HttpPost]
        public ActionResult ThemMucYeuThich(int IDSanPham)
        {
            
            
            if (Session["TaiKhoan"] != null)
            {
                var sanPham = DongHoDatabase.Instance.Product.FirstOrDefault(sp => sp.IDSanpham == IDSanPham);

                var khachHang = (Customer)Session["TaiKhoan"];
                var yeuThich = DongHoDatabase.Instance.Favorite.FirstOrDefault(yt => yt.IDSanPham == IDSanPham && yt.IDUser == khachHang.IDUser );
                
                var addToFavoriteCommand = new AddtoFavorite(khachHang.IDUser, IDSanPham );
                addToFavoriteCommand.Execute();

            }
            else
            {
                return RedirectToAction("Login", "LoginRegister");
            }

            return RedirectToAction("Index", "Favorite");
        }
        [HttpPost]
        public ActionResult XoaYeuThich(int IDSanPham)
        {
            // Tìm bản ghi yêu thích theo ID sản phẩm và thực hiện xóa
            if(Session["TaiKhoan"]!= null)
            {
                var kh = (Customer)Session["TaiKhoan"];
                var yeuThich = DongHoDatabase.Instance.Favorite.FirstOrDefault(y => y.IDSanPham == IDSanPham && y.IDUser == kh.IDUser);
                if (yeuThich != null)
                {
                    DongHoDatabase.Instance.Favorite.Remove(yeuThich);
                    DongHoDatabase.Instance.SaveChanges();
                }
                var favoriteProducts = DongHoDatabase.Instance.Favorite.Where(y => y.IDUser == kh.IDUser).ToList(); ;
                return View("Index", favoriteProducts);
            }
            else
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            

            // Trả về View danh sách yêu thích sau khi xóa
            /*var favoriteProducts = db.Favorite.Include("Favorite").ToList();
            return View("Index", favoriteProducts);*/
        }
       
    }
}