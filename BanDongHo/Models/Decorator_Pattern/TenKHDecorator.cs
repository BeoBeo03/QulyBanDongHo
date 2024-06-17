using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class TenKHDecorator : AbstractDecorator
    {
        Customer cus1;
        public TenKHDecorator(AbstractCustomer cus, string tenKH, Customer cus1) : base(cus)
        {
            this.cus1 = cus1;
            TenKH = tenKH;
        }
        public override Customer MakeCustomer()
        {
            base.MakeCustomer();
            Customer cus = cus1;
            cus.TenKH = TenKH;
            return cus;
        }
    }
}