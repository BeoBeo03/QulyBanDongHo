using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.State_Pattern
{
    public class PendingState : IOrderState
    {
        public void ApproveOrder(DonHang order)
        {
            // Cập nhật trạng thái của đơn hàng thành "Đã duyệt" và ngày giao hàng
            order.TinhTrang = "Đã duyệt";
            order.NgayGiao = DateTime.Now;
        }
    }
}