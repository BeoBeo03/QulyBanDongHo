using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class HomePageController : Controller
    {
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        // GET: HomePage
        public ActionResult Index(string SearchString)
        {
            var products = db.Product.ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.TenSP.Contains(SearchString)).ToList();
            }
            return View(products);
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
    }
}