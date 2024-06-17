using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public abstract class AbstractCustomer
    {
        //Lớp trừu tượng chứa thuộc tính khách hàng 
        public int IDUser { get; set; }
        public string TenKH { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public int SDT { get; set; }
        //Khai báo lớp trừu tượng
        public abstract Customer MakeCustomer(); 
    }
}