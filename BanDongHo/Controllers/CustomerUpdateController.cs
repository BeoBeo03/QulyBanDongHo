using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models;

namespace BanDongHo.Controllers
{
    public class CustomerUpdateController : Controller
    {
        // GET: CustomerUpdate
        DoAnPM_LTEntities db = new DoAnPM_LTEntities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var userID = (Customer)Session["TaiKhoan"];
                var customer = db.Customer.Where(yt => yt.IDUser == userID.IDUser).ToList();
                return View(customer);
            }

            return RedirectToAction("Index", "CustomerUpdate");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "IDUser,TenKH,GioiTinh,Email,SDT")] Customer customer)
            
        {
            if (ModelState.IsValid)
            {
                Customer customerUpdate = db.Customer.Find(id);
                customerUpdate.TenKH = customer.TenKH;
                customerUpdate.GioiTinh = customer.GioiTinh;
                customer.SDT = customer.SDT;
                customerUpdate.Email = customer.Email;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            db.SaveChanges();
            return View(customer);
        }
    }
}