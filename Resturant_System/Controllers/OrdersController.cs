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
    public class OrdersController : Controller
    {
        private OrderDBContext db = new OrderDBContext();
        private MealDBContext db_meal = new MealDBContext();
        private InvoiceDBContext db_invoce = new InvoiceDBContext();
        private TableDBContext db_table = new TableDBContext();
        

        //
        // GET: /Orders/

        public ActionResult Index()
        {
            meal_intoView();
            invoice_intoView();
            return View(db.Order.ToList());
        }

        //
        // GET: /Orders/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            meal_intoView();
            invoice_intoView();
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            meal_intoView();
            invoice_intoView();
            return View();
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public int CreateOrderRetId(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                Order a = (from b in db.Order select b).ToList().Last();
                return a.Id;
            }

            return -1;
        }
        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            meal_intoView();
            invoice_intoView();
            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
        void meal_intoView()
        {
            // To add dropdown list of Meals
            var myMeals = from m in db_meal.Meal select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myMeals)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.Meal_Ids = lst_Select_Item;
        }
        void invoice_intoView()
        {
            // To add dropdown list of Invoices
            var myInvoices = from m in db_invoce.Invoice select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myInvoices)
            {
                var s= (from a in db_table.Table where a.Id == item.Table_Id select a).First().Unit_Name;
                lst_Select_Item.Add(new Select_Item(item.Id, " Table :: " + s, false));
            }
                
            ViewBag.Invoice_Ids = lst_Select_Item;
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            meal_intoView();
            invoice_intoView();
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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