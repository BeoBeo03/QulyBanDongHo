using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models.Strategy_Pattern
{
    public class NameSearchStrategy : ISearchStrategy
    {
        public List<Product> Search(string keyword)
        {
            //Triển khai giao diện ISearchStrategy và tìm kiếm bằng từ khóa 
            return DongHoDatabase.Instance.Product.Where(n => n.TenSP.Contains(keyword)).ToList();
        }
    }
}