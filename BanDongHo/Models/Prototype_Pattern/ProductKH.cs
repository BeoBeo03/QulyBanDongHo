using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models
{
    public class ProductKH : ProductPrototype
    {
        public int ID { get; set; }

        // Các thuộc tính khác của sản phẩm
        //Triển khai phương thức Clone
        public override ProductPrototype Clone()
        {
            return (ProductKH)this.MemberwiseClone();
        }

        public override List<Product> SetProduct(int? id)
        {
            
            /* var sanphams = db.Product.ToList();*/
            var sanphams = DongHoDatabase.Instance.Product.ToList();
            return new List<Product>(sanphams.OrderBy(sanpham => sanpham.IDSanpham));
        }
    }
}