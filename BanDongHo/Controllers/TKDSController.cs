using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class TKDSController : Controller
    {
        // GET: TKDS
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public static string connectionString = "data source=LEANHKHOA\\SQLEXPRESS;initial catalog=DoAnPM_LT;integrated security=True";
        public ActionResult Index()
        {
            ViewBag.CountOrderDetail = db.CTDonHang.Count();
            ViewBag.CountProduct = db.Product.Count();

            TempData["TongDonHang"] = db.DonHang.Where(n => n.TinhTrang == "Đã duyệt").Count();

            TempData["TongDoanhThu"] = db.DonHang.Where(n => n.TinhTrang == "Đã duyệt" /*&& n.NgayGiao.ToString() != ""*/).Sum(n => n.TongThanhTien);
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            else
            {

                string query = "TKSP";
                DateTime fromDate = new DateTime(2023, 08, 01);
                DateTime toDate = new DateTime(2023, 12, 31);

                List<ThongKeTheoTuanModel> model = new List<ThongKeTheoTuanModel>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Xử lý dữ liệu từ SqlDataReader
                    while (reader.Read())
                    {
                        ThongKeTheoTuanModel item = new ThongKeTheoTuanModel();
                        item.TuNgay = (DateTime)reader["TuNgay"];
                        item.DenNgay = (DateTime)reader["DenNgay"];
                        item.SoHoaDon = (int)reader["SoHoaDon"];
                        item.TongDoanhThu = (decimal)reader["TongDoanhThu"];
                        model.Add(item);
                    }
                    reader.Close();
                }

                return View(model);
            }
        }
    }
}