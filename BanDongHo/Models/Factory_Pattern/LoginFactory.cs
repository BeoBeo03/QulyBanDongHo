using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Factory_Pattern
{
    public abstract class LoginFactory<T>
    {
        public abstract ILogin<T> CreateLogin();//dùng để gọi ra 
    }
}