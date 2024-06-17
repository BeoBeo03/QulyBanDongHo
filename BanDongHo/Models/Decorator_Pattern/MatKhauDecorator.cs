using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class MatKhauDecorator : AbstractDecorator
    {
        Customer cus1;
        public MatKhauDecorator(AbstractCustomer cus, string matKhau, Customer cus1) : base(cus)
        {
            this.cus1 = cus1;
            MatKhau = matKhau;
        }
        public override Customer MakeCustomer()
        {
            base.MakeCustomer();
            Customer cus = cus1;
            cus.MatKhau = MatKhau;
            return cus;
        }
    }
}