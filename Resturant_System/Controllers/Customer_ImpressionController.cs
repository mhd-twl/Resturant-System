using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resturant_System.Models;

namespace Resturant_System.Controllers
{
    public class Customer_ImpressionController : Controller
    {
        private Customer_ImpressionDBContext db = new Customer_ImpressionDBContext();

        //
        // GET: /Customer_Impression/

        public ActionResult Index()
        {
            return View(db.Customer_Impressions.ToList());
        }

        //
        // GET: /Customer_Impression/Details/5

        public ActionResult Details(int id = 0)
        {
            Customer_Impression customer_impression = db.Customer_Impressions.Find(id);
            if (customer_impression == null)
            {
                return HttpNotFound();
            }
            return View(customer_impression);
        }

        //
        // GET: /Customer_Impression/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer_Impression/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer_Impression customer_impression)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Impressions.Add(customer_impression);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer_impression);
        }

        //
        // GET: /Customer_Impression/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Customer_Impression customer_impression = db.Customer_Impressions.Find(id);
            if (customer_impression == null)
            {
                return HttpNotFound();
            }
            return View(customer_impression);
        }

        //
        // POST: /Customer_Impression/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer_Impression customer_impression)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_impression).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer_impression);
        }

        //
        // GET: /Customer_Impression/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Customer_Impression customer_impression = db.Customer_Impressions.Find(id);
            if (customer_impression == null)
            {
                return HttpNotFound();
            }
            return View(customer_impression);
        }

        //
        // POST: /Customer_Impression/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Impression customer_impression = db.Customer_Impressions.Find(id);
            db.Customer_Impressions.Remove(customer_impression);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}