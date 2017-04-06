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
    public class InvoiceStatusController : Controller
    {
        private Invoice_StatusDBContext db = new Invoice_StatusDBContext();

        //
        // GET: /InvoiceStatus/

        public ActionResult Index()
        {
            return View(db.Invoice_Status.ToList());
        }

        //
        // GET: /InvoiceStatus/Details/5

        public ActionResult Details(int id = 0)
        {
            Invoice_Status invoice_status = db.Invoice_Status.Find(id);
            if (invoice_status == null)
            {
                return HttpNotFound();
            }
            return View(invoice_status);
        }

        //
        // GET: /InvoiceStatus/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InvoiceStatus/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invoice_Status invoice_status)
        {
            if (ModelState.IsValid)
            {
                db.Invoice_Status.Add(invoice_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoice_status);
        }

        //
        // GET: /InvoiceStatus/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Invoice_Status invoice_status = db.Invoice_Status.Find(id);
            if (invoice_status == null)
            {
                return HttpNotFound();
            }
            return View(invoice_status);
        }

        //
        // POST: /InvoiceStatus/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice_Status invoice_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice_status);
        }

        //
        // GET: /InvoiceStatus/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Invoice_Status invoice_status = db.Invoice_Status.Find(id);
            if (invoice_status == null)
            {
                return HttpNotFound();
            }
            return View(invoice_status);
        }

        //
        // POST: /InvoiceStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice_Status invoice_status = db.Invoice_Status.Find(id);
            db.Invoice_Status.Remove(invoice_status);
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