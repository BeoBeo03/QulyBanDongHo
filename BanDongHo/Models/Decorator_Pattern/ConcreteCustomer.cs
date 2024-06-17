using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class ConcreteCustomer : AbstractCustomer
    {
        //Lớp này khởi tạo các giá trị măc định cho các thuộc tính KH
        public ConcreteCustomer()
        {
            TenKH = "";
            GioiTinh = "";
            Email = "";
            MatKhau = "";
            SDT = 0;
            
        }
        public override Customer MakeCustomer()
        {
            Customer cus = new Customer();
            cus.TenKH = TenKH;
            cus.GioiTinh = GioiTinh;
            cus.Email = Email;
            cus.MatKhau = MatKhau;
            cus.SDT = SDT;
            return cus;
        }
    }
}