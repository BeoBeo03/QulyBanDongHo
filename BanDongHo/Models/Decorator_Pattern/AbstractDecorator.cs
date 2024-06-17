using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Decorator_Pattern
{
    public class AbstractDecorator : AbstractCustomer
    {
        AbstractCustomer cus;
        public AbstractDecorator(AbstractCustomer cus)
        {
            this.cus = cus;
        }
        public override Customer MakeCustomer()
        {
            return cus.MakeCustomer();
        }
    }
}