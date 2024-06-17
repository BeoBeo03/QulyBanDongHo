using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.State_Pattern
{
    public interface IOrderState
    {
        //Lớp giao diện cần được triển khai
        void ApproveOrder(DonHang order);//Định nghĩa 1 hvi đơn hàng được duyệt

    }
}