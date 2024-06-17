using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Factory_Pattern
{
    public  class LoginUser : LoginFactory<Customer>
    {
        public override ILogin<Customer> CreateLogin()// triển khai phương thức createLogin 
        {
            return new User();//Tạo 1 phiên bản  mới của user
        }
    }
}