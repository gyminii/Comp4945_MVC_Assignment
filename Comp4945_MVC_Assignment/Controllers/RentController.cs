using Comp4945_MVC_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comp4945_MVC_Assignment.Controllers
{
    public class RentController : Controller
    {

        CarRentalEntities db = new CarRentalEntities();
        // GET: Rent
        public ActionResult Index()
        {
            var result = (from r in db.rentails
                          join c in db.carregs on r.carid equals c.carno
                          select new RentailViewModel
                          {
                              id = r.id,
                              carid = r.carid,
                              custid = r.custid,
                              fee = r.fee,
                              sdate = r.sdate,
                              edate = r.edate,
                              available = c.available
                          }).ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult Getcar()
        {
            var car = db.carregs.ToList();
            return Json(car, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getid(int id)
        {
            var customer = (from s in db.customers where s.id == id select s.custname).ToList();

            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getavail(string carno)
        {
            var caravail = (from s in db.carregs where s.carno == s.carno select s.available).FirstOrDefault();

            return Json(caravail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(rentail rent)
        {
            if (ModelState.IsValid)
            {
                db.rentails.Add(rent);
                var car = db.carregs.SingleOrDefault(e => e.carno == rent.carid);
                if (car == null)
                {
                    return HttpNotFound("CarNo is not valid");
                }
                car.available = "no";
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(rent);
        }

    }
}