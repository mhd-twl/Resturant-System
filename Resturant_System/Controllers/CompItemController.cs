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
    public class CompItemController : Controller
    {
        private Comp_ItemDBContext db = new Comp_ItemDBContext();
        private ComponentDBContext db_cmp = new ComponentDBContext();
        //
        // GET: /CompItem/

        public ActionResult Index()
        {
            component_intoView();
            return View(db.Comp_Item.ToList());
        }

        //
        // GET: /CompItem/Details/5

        public ActionResult Details(int id = 0)
        {
            Comp_Item comp_item = db.Comp_Item.Find(id);
            component_intoView();
            if (comp_item == null)
            {
                return HttpNotFound();
            }
            return View(comp_item);
        }

        //
        // GET: /CompItem/Create

        public ActionResult Create()
        {
            component_intoView();
            return View();
        }

        //
        // POST: /CompItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comp_Item comp_item)
        {
            if (ModelState.IsValid)
            {
                db.Comp_Item.Add(comp_item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comp_item);
        }

        //
        // GET: /CompItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Comp_Item comp_item = db.Comp_Item.Find(id);
            component_intoView();
            if (comp_item == null)
            {
                return HttpNotFound();
            }
            return View(comp_item);
        }

        //
        // POST: /CompItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comp_Item comp_item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comp_item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comp_item);
        }

        //
        // GET: /CompItem/Delete/5
        void component_intoView()
        {
            // To add dropdown list of Meal_Category
            var myCatgs = from m in db_cmp.Components select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myCatgs)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.ComponentID = lst_Select_Item;
        }
        public ActionResult Delete(int id = 0)
        {
            Comp_Item comp_item = db.Comp_Item.Find(id);
            if (comp_item == null)
            {
                return HttpNotFound();
            }
            return View(comp_item);
        }

        //
        // POST: /CompItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comp_Item comp_item = db.Comp_Item.Find(id);
            db.Comp_Item.Remove(comp_item);
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