using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class SDTDecorator : AbstractDecorator
    {
        Customer cus1;
        public SDTDecorator(AbstractCustomer cus, int sdt, Customer cus1) : base(cus)
        {
            this.cus1 = cus1;
            SDT = sdt;
        }
        public override Customer MakeCustomer()
        {
            base.MakeCustomer();
            Customer cus = cus1;
            cus.SDT = SDT;
            return cus;
        }
    }
}