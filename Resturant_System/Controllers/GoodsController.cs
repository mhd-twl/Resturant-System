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
    public class GoodsController : Controller
    {
        private GoodsDBContext db = new GoodsDBContext();

        //
        // GET: /Goods/

        public ActionResult Index()
        {
            return View(db.Goods.ToList());
        }

        //
        // GET: /Goods/Details/5

        public ActionResult Details(int id = 0)
        {
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        //
        // GET: /Goods/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Goods/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Goods goods)
        {
            if (ModelState.IsValid)
            {
                db.Goods.Add(goods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goods);
        }

        //
        // GET: /Goods/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        //
        // POST: /Goods/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Goods goods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goods);
        }

        //
        // GET: /Goods/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        //
        // POST: /Goods/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goods goods = db.Goods.Find(id);
            db.Goods.Remove(goods);
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