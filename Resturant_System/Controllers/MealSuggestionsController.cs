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
    public class MealSuggestionsController : Controller
    {
        private Meal_SuggestionDBContext db = new Meal_SuggestionDBContext();
        private Meal_CategoryDBContext db_catgory = new Meal_CategoryDBContext();

        //
        // GET: /MealSuggestions/

        public ActionResult Index()
        {
            meal_categorys_intoView();
            return View(db.Meal_Suggestion.ToList());
        }

        //
        // GET: /MealSuggestions/Details/5

        public ActionResult Details(int id = 0)
        {
            Meal_Suggestion meal_suggestion = db.Meal_Suggestion.Find(id);
            if (meal_suggestion == null)
            {
                return HttpNotFound();
            }
            meal_categorys_intoView();
            return View(meal_suggestion);
        }

        //
        // GET: /MealSuggestions/Create

        public ActionResult Create()
        {
            meal_categorys_intoView();
            return View();
        }

        //
        // POST: /MealSuggestions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meal_Suggestion meal_suggestion)
        {
            if (ModelState.IsValid)
            {
                db.Meal_Suggestion.Add(meal_suggestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal_suggestion);
        }

        //
        // GET: /MealSuggestions/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Meal_Suggestion meal_suggestion = db.Meal_Suggestion.Find(id);
            if (meal_suggestion == null)
            {
                return HttpNotFound();
            }
            meal_categorys_intoView();
            return View(meal_suggestion);
        }

        //
        // POST: /MealSuggestions/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Meal_Suggestion meal_suggestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal_suggestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal_suggestion);
        }
        // // Get List Of Meals_Category into View category 
        void meal_categorys_intoView()
        {
            // To add dropdown list of Meal_Category
            var myCatgs = from m in db_catgory.Meal_Category select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myCatgs)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.CategoryID = lst_Select_Item;
        }
        //
        // GET: /MealSuggestions/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Meal_Suggestion meal_suggestion = db.Meal_Suggestion.Find(id);
            if (meal_suggestion == null)
            {
                return HttpNotFound();
            }
            return View(meal_suggestion);
        }

        //
        // POST: /MealSuggestions/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal_Suggestion meal_suggestion = db.Meal_Suggestion.Find(id);
            db.Meal_Suggestion.Remove(meal_suggestion);
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