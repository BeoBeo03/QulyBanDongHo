using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class GioiTinhDecorator : AbstractDecorator
    {
        Customer cus1;
        public GioiTinhDecorator(AbstractCustomer cus, string gioiTinh, Customer cus1) : base(cus)
        {
            this.cus1 = cus1;
            GioiTinh = gioiTinh;
        }
        public override Customer MakeCustomer()
        {
            base.MakeCustomer();
            Customer cus = cus1;
            cus.GioiTinh = GioiTinh;
            return cus;
        }
    }
}