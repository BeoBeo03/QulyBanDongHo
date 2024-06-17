using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Factory_Pattern
{
    public interface ILogin<T>
    {
        bool DangNhap(string taiKhoan);
        bool DangNhap(T x, ref object taikhoan);
    }
}