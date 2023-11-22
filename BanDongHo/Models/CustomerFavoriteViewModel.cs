using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models
{
    public class CustomerFavoriteViewModel
    {
        public int IDYeuThich { get; set; }
        public int IDUser { get; set; }
        public string CustomerName { get; set; } // Tên khách hàng
        public int FavoriteCount { get; set; } // Số lượng sản phẩm yêu thích
        public Favorite Favorite { get; set; }
        public Customer Customer { get; set; }
    }
}