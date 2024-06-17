using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Strategy_Pattern
{
    public interface ISearchStrategy
    {
        List<Product> Search(string keyword);//Giao diện định nghĩa phương thức search bằng từ khóa
    }
}