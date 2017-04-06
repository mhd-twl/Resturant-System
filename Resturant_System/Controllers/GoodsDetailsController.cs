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
    public class GoodsDetailsController : Controller
    {
        private Goods_CategoryDBContext db = new Goods_CategoryDBContext();

        //
        // GET: /GoodsDetails/

        public ActionResult Index()
        {
            return View(db.Goods_Details.ToList());
        }

        //
        // GET: /GoodsDetails/Details/5

        public ActionResult Details(int id = 0)
        {
            Goods_Details goods_details = db.Goods_Details.Find(id);
            if (goods_details == null)
            {
                return HttpNotFound();
            }
            return View(goods_details);
        }

        //
        // GET: /GoodsDetails/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GoodsDetails/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Goods_Details goods_details)
        {
            if (ModelState.IsValid)
            {
                db.Goods_Details.Add(goods_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goods_details);
        }

        //
        // GET: /GoodsDetails/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Goods_Details goods_details = db.Goods_Details.Find(id);
            if (goods_details == null)
            {
                return HttpNotFound();
            }
            return View(goods_details);
        }

        //
        // POST: /GoodsDetails/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Goods_Details goods_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goods_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goods_details);
        }

        //
        // GET: /GoodsDetails/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Goods_Details goods_details = db.Goods_Details.Find(id);
            if (goods_details == null)
            {
                return HttpNotFound();
            }
            return View(goods_details);
        }

        //
        // POST: /GoodsDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goods_Details goods_details = db.Goods_Details.Find(id);
            db.Goods_Details.Remove(goods_details);
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