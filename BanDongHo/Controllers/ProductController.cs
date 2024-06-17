
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{

    public  class ProductController : Controller
    {
        // GET: Product
        /*private readonly DoAnPM_LTEntities db = new DoAnPM_LTEntities();*/
        public ActionResult Index(int? id)
        {
            ProductFactory productFactory = new ProductFactory(new ProductKH());// tạo productfactory với nguyên mẫu productkh

            // Lấy một bản sao của đối tượng nguyên mẫu
            ProductPrototype productPrototype = productFactory.GetCustomerProduct();

            // Gọi phương thức SetProduct để lấy danh sách sản phẩm từ cơ sở dữ liệu
            var productList = productPrototype.SetProduct(id);
            

            // Kiểm tra xem sản phẩm có tồn tại không
            var product = productList.FirstOrDefault(p => p.IDSanpham == id);
            if (product == null)
            {
                return Content("Không tìm thấy sản phẩm");
            }
            var relatedProducts = productList
                            .Where(p => p.Category == product.Category && p.IDSanpham != id)
                            .Take(4)
                            .ToList();
            // Truyền danh sách sản phẩm liên quan vào ViewBag
            ViewBag.RelatedProducts = relatedProducts;
            // Trả về view với sản phẩm đã chọn
            return View(product);
        }
        public ActionResult SanPhamTheoPhanLoai(string phanloai)
        {
            // Thực hiện truy vấn SQL để lấy sản phẩm theo phân loại
            /*  List<Product> products = db.Product.Where(p => p.Category.TenPhanLoai == phanloai).ToList();*/
            List<Product> products = DongHoDatabase.Instance.Product.Where(p => p.Category.TenPhanLoai == phanloai).ToList();
            // Truyền danh sách sản phẩm đã lọc vào view
            ViewBag.CategoryName = DongHoDatabase.Instance.Category.FirstOrDefault().TenPhanLoai;
            return View(products);

        }


        

        // Action để hiển thị sản phẩm theo phân loại
        
    }
}