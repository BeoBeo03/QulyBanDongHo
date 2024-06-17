using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class EmailDecorator : AbstractDecorator
    {

        Customer cus1;
        public EmailDecorator(AbstractCustomer cus, string email, Customer cus1) : base(cus)
        {
            this.cus1 = cus1;
            Email = email;
        }
        public override Customer MakeCustomer()
        {
            base.MakeCustomer();
            Customer cus = cus1;
            cus.Email = Email;
            return cus;
        }
    }
}