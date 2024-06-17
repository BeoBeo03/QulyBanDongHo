using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Models
{
    public class ProductFactory
    {
        //Tạo ra phiên bản ProductPrototype
        private readonly ProductPrototype custumerProduct;

        public ProductFactory(ProductPrototype product)
        {
            custumerProduct = product;
        }

        public ProductPrototype GetCustomerProduct()
        {
            return custumerProduct.Clone();
        }
    }
}