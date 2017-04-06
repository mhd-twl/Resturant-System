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
    public class InvoiceController : Controller
    {
        private InvoiceDBContext db = new InvoiceDBContext();
        private Invoice_StatusDBContext db_invc = new Invoice_StatusDBContext();
        private TableDBContext db_table = new TableDBContext();

        //
        // GET: /Invoice/

        public ActionResult Index()
        {
            invoice_status_intoView();
            table_intoView();
            return View(db.Invoice.ToList());
        }

        //
        // GET: /Invoice/Details/5

        public ActionResult Details(int id = 0)
        {
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            invoice_status_intoView();
            table_intoView();
            return View(invoice);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {
            invoice_status_intoView();
            table_intoView();
            return View();
        }

        //
        // POST: /Invoice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invoice invoice)
        {
            invoice_status_intoView();
            table_intoView();
            if (ModelState.IsValid)
            {
                db.Invoice.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoice);
        }
        public int CreateInvoiceRetId(Invoice invoice)
        {
            invoice_status_intoView();
            table_intoView();
            if (ModelState.IsValid)
            {
                db.Invoice.Add(invoice);
                db.SaveChanges();

                Invoice a = (from b in db.Invoice select b).ToList().Last();
                return a.Id;
            }

            return -1;
        }

        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            invoice_status_intoView();
            table_intoView();
            return View(invoice);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            invoice_status_intoView();
            table_intoView();
            return View(invoice);
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoice.Find(id);
            db.Invoice.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        void invoice_status_intoView()
        {
            // To add dropdown list of Meal_Category
            var myInvSts = from m in db_invc.Invoice_Status select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myInvSts)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.InvSts = lst_Select_Item;
        }
        void table_intoView()
        {
            // To add dropdown list of Meal_Category
            var myTable = from m in db_table.Table select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myTable)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Unit_Name, false));
            ViewBag.Tables = lst_Select_Item;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}