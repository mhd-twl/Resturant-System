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
    public class MealContentsController : Controller
    {
        private Meal_ContentDBContext db = new Meal_ContentDBContext();
        private MealDBContext db_Meal = new MealDBContext();
        private GoodsDBContext db_Goods = new GoodsDBContext();
        
        

        //
        // GET: /MealContents/

        public ActionResult Index()
        {
            meal_intoView();
            Goods_intoView();
            return View(db.Meal_Content.ToList());
        }

        //
        // GET: /MealContents/Details/5

        public ActionResult Details(int id = 0)
        {
            Meal_Content meal_content = db.Meal_Content.Find(id);
            if (meal_content == null)
            {
                return HttpNotFound();
            }
            meal_intoView();
            Goods_intoView();
            return View(meal_content);
        }

        //
        // GET: /MealContents/Create

        public ActionResult Create()
        {
            meal_intoView();
            Goods_intoView();
            return View();
        }

        //
        // POST: /MealContents/Create


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult Create(Meal_Content meal_content)
        {
            if (ModelState.IsValid)
            {
                db.Meal_Content.Add(meal_content);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal_content);
        }
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "CreateMore")]
        public ActionResult CreateMore(Meal_Content meal_content)
        {
            meal_intoView();
            Goods_intoView();
            if (ModelState.IsValid)
            {
                meal_content.Id = 0;
                db.Meal_Content.Add(meal_content);
                db.SaveChanges();
                return View(meal_content);
            }

            return View(meal_content);
        }
        //
        // GET: /MealContents/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Meal_Content meal_content = db.Meal_Content.Find(id);
            if (meal_content == null)
            {
                return HttpNotFound();
            }
            meal_intoView();
            Goods_intoView();
            return View(meal_content);
        }

        //
        // POST: /MealContents/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Meal_Content meal_content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal_content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal_content);
        }

        //
        // GET: /MealContents/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Meal_Content meal_content = db.Meal_Content.Find(id);
            if (meal_content == null)
            {
                return HttpNotFound();
            }
            meal_intoView();
            Goods_intoView();
            return View(meal_content);
        }


        
        void meal_intoView() {
            // To add dropdown list of Meals_
            var myMeals = from m in db_Meal.Meal select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myMeals)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.Meal_Ids = lst_Select_Item;
        }
        void Goods_intoView()
        {
            // To add dropdown list of Goods_
            var myGoodses = from m in db_Goods.Goods select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myGoodses)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.Goods_Ids = lst_Select_Item;
        }

        //
        // POST: /MealContents/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal_Content meal_content = db.Meal_Content.Find(id);
            db.Meal_Content.Remove(meal_content);
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