using Microsoft.Ajax.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanDongHo.Models
{
    public abstract class ProductPrototype 
    {
        //Khai báo 2 phương thức trừu tượng
        public abstract ProductPrototype Clone();
        public abstract List<Product> SetProduct(int? id);//Lấy sản phẩm từ csdl
    }
    
}