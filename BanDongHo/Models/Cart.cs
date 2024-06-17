using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Models;

namespace BanDongHo.Models
{
    public class CartItem
    {
        public Product _sanpham { get; set; }
        public int _soluong { get; set; }
    }
    //Gọi sản phẩm ra
    public class Cart
    {
        public string UserID { get; set; }
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void AddtoCart(Product _sp, int _sl = 1)
        {
            var item = Items.FirstOrDefault(s => s._sanpham.IDSanpham == _sp.IDSanpham);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _sanpham = _sp,
                    _soluong = _sl,
                });
            }
            else
                item._soluong += _sl;
        }
        public int Total_quantity()
        {
            return items.Sum(s => s._soluong);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s._soluong * s._sanpham.GiaSP);
            return (decimal)total;
        }
        public void UpdateNewQuantity(int id, int new_quantity)
        {
            var up_item = items.Find(s => s._sanpham.IDSanpham == id);
            if (up_item != null)
                up_item._soluong = new_quantity;
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._sanpham.IDSanpham == id);

        }
        public void ClearCart()
        {
            items.Clear();
        }

        internal int Sum(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}