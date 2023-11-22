using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class AdminFavoriteController : Controller
    {
        // GET: AdminFavorite
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();

        // GET: AdminFavorite
        public ActionResult Index()
        {
            // Truy vấn cơ sở dữ liệu để lấy danh sách khách hàng và số lượng sản phẩm yêu thích của mỗi khách hàng
            var favoriteCounts = db.Favorite.GroupBy(f => f.IDUser)
                .Select(g => new
                {
                    UserID = g.Key,
                    FavoriteCount = g.Count()
                })
                .ToList();

            // Lấy danh sách khách hàng từ cơ sở dữ liệu
            var customers = db.Customer.ToList();
            var product = db.Product.ToList();
            // Tạo một danh sách ViewModel để hiển thị thông tin yêu thích của khách hàng
            List<CustomerFavoriteViewModel> adminFavoriteViewModels = new List<CustomerFavoriteViewModel>();

            foreach (var customer in customers)
            {
                var favoriteCount = favoriteCounts.FirstOrDefault(fc => fc.UserID == customer.IDUser);
                int count = favoriteCount != null ? favoriteCount.FavoriteCount : 0;

                var viewModel = new CustomerFavoriteViewModel
                {

                    IDUser = customer.IDUser,
                    CustomerName = customer.TenKH,
                    FavoriteCount = count
                };

                adminFavoriteViewModels.Add(viewModel);
            }

            return View(adminFavoriteViewModels);
        }
        public ActionResult CustomerFavorite(int mayt, string timkiem)
        {

            if (timkiem != null)
            {
                List<Favorite> listKQ = db.Favorite.Where(n => n.IDSanPham.ToString().Contains(timkiem)).ToList();
                if (listKQ.Count == 0)
                {
                    TempData["thongbao"] = "Không tìm thấy đơn hàng nào phù hợp.";
                    return View(db.Favorite.Where(n => n.IDSanPham == mayt));

                }
                return View(listKQ.OrderByDescending(n => n.IDSanPham == mayt));

            }
            return View(db.Favorite.Where(n => n.IDSanPham == mayt));

        }
        public ActionResult Error()
        {
            return View();
        }
    }
}