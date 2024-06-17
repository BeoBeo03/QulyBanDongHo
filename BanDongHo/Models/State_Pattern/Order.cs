using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.State_Pattern
{
    public class Order
    {
        //Đối tượng cần thay đổi hành vi
        public int IDDonHang { get; set; }
        public string TinhTrang { get; set; }
        public DateTime? NgayGiao { get; set; }

        private IOrderState _state;

        public Order(IOrderState state)//Trạng thái ban đầu đối tượng
        {
            _state = state;
        }

        public void SetState(IOrderState state)// thay đổi trạng thái đối tượng
        {
            _state = state;
        }

        public void ApproveOrder(DonHang order)
        {
            _state.ApproveOrder(order);
        }
    }
}