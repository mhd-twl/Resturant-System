using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resturant_System.Models;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Resturant_System.Controllers
{
    public class MealsViewController : Controller
    {
        MealsViewDBContext db = new MealsViewDBContext();

        Meal_CategoryDBContext dbMealsCatg = new Meal_CategoryDBContext();
        MealDBContext dbMeals = new MealDBContext();
        //may needs
        OrderDBContext dbOrders = new OrderDBContext();
        TableDBContext dbTables = new TableDBContext();
        InvoiceDBContext dbInvoices = new InvoiceDBContext();

        private List<MealsView> fill_data()
        {
            List<MealsView> lst = new List<MealsView>();
            var g = from a in dbMealsCatg.Meal_Category select a;
            var m = from a in dbMeals.Meal select a;
            foreach (var ctg in g)
            {
                List<Meal> lsMeals = new List<Meal>();
                foreach (var ml in m)
	            {
                    if (ml.CategoryId == ctg.Id)
                    {
                        lsMeals.Add(ml);
                    }
	            }
                lst.Add(new MealsView(ctg, lsMeals));
            }
            return lst;
        }

        
        public ActionResult Index()
        {
            return View(fill_data());
        }

        


        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MealsView/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MealsView/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MealsView/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MealsView/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MealsView/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
