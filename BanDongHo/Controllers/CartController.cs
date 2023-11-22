using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class CartController : Controller
    {
        private DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        // GET: Cart
        public ActionResult Index()
        {
            
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            else
            {


                Cart cart = Session["Cart"] as Cart;
                if (cart == null || cart.Total_quantity() == 0)
                    return RedirectToAction("EmptyCart", "Cart");
                return View(cart);

            }
            
        }
        public ActionResult AddToCart(int id)
        {
            if(Session["TaiKhoan"] != null)
            {
                int userID = ((Customer)Session["TaiKhoan"]).IDUser;
                var _spham = db.Product.SingleOrDefault(s => s.IDSanpham == id);
                if (_spham != null)
                {
                    if (_spham.TongSoLuong > 0)
                    {
                        /*GetCart().UserID = userID;*/
                        GetCart().AddtoCart(_spham);
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        // Hiển thị thông báo khi số lượng đặt hàng lớn hơn số lượng tồn kho
                        TempData["OutOfStockMessage"] = "Sản phẩm này đã hết hàng!";
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            
            return RedirectToAction("Index", "Cart");
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult UpdateCartQua(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_sp = int.Parse(Request.Form["idPro"]);
            int new_quantity = int.Parse(Request.Form["cartQuantity"]);
            var product = db.Product.SingleOrDefault(p => p.IDSanpham == id_sp);

            // Kiểm tra số lượng tồn kho trước khi cập nhật
            if (product != null && new_quantity <= product.TongSoLuong)
            {
                cart.UpdateNewQuantity(id_sp, new_quantity);
            }
            else
            {
                // Hiển thị thông báo khi số lượng cập nhật vượt quá số lượng tồn kho
                TempData["OutOfStockMessage"] = "Số lượng cập nhật vượt quá số lượng tồn kho!";
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;

            cart.Remove_CartItem(id);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult OrderDetail()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            else
            {
                if (Session["Cart"] == null)
                    return View("Index");
                Cart cart = Session["Cart"] as Cart;
                return View(cart);
            }
           
        }

        public ActionResult DongYDatHang(FormCollection form)
        {
            if (form != null)
            {
                String ten = Request.Form["hoten"].ToString();
                String diachi = Request.Form["diachigiao"].ToString();
                if (ten == "")
                {
                    TempData["loiten"] = "Vui lòng nhập tên!";
                    return RedirectToAction("OrderDetail", "Cart");
                }
                if (diachi == "")
                {
                    TempData["loidiachi"] = "Vui lòng nhập đại chỉ!";
                    return RedirectToAction("OrderDetail", "Cart");
                }
                
                Customer user = Session["TaiKhoan"] as Customer;
                Cart cart = Session["Cart"] as Cart;
                DonHang _order = new DonHang();
                _order.Ten = ten;
                _order.IDUser = user.IDUser;
                _order.DCGiao = diachi;
                
                /*_order.TenKH = form["TenKH"];
                _order.SDT = (form["SDT"]);*/
                _order.TongSoLuong = cart.Total_quantity();
                _order.NgayBan = DateTime.Now;
                _order.TinhTrang = "Chưa duyệt";
                _order.TongThanhTien = (decimal)cart.Total_money();
                // Lấy số tiền khách hàng nhập vào từ form

                // Tính tiền thối
               


                db.DonHang.Add(_order);
                db.SaveChanges();

                foreach (var item in cart.Items)
                {
                    // lưu dòng sản phẩm vào chi tiết hóa đơn
                    CTDonHang _order_detail = new CTDonHang();
                    
                    _order_detail.IDDonHang = _order.IDDonHang;
                    _order_detail.IDSanpham = item._sanpham.IDSanpham;
                   
                    _order_detail.TenSP = item._sanpham.TenSP;
                    _order_detail.Soluong = item._soluong;
                    _order_detail.GiaTien = item._sanpham.GiaSP;
                    _order_detail.ThanhTien = item._soluong * item._sanpham.GiaSP;
                    var product = db.Product.Find(item._sanpham.IDSanpham);
                    if (product != null)
                    {
                        product.TongSoLuong -= item._soluong;

                        if (product.TongSoLuong == 0 || product.TongSoLuong < 0)
                        {
                            product.TongSoLuong = 0;
                        }
                    }
                    db.CTDonHang.Add(_order_detail);
                    //Số lượng tồn khi đặt mua hàng sẽ trừ vào tổng số lượng
                    db.SaveChanges();


                }

                
                cart.ClearCart();

                return RedirectToAction("CheckOut_Success", "Cart");
            }
            else
            {
                return Content("Có sai sót! Xin kiểm tra lại thông tin");

            }
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
        public ActionResult EmptyCart()
        {
            ViewBag.EmptyNotification = "Chưa có sản phẩm nào trong giỏ hàng";
            return View();
        }

    }
}