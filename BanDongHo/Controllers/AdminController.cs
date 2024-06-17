using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] != null)
            {
                var customer = db.Customer;
                return View(customer.ToList());
            }
            return RedirectToAction("Login", "LoginRegister");
        }
    }
}