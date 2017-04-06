using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resturant_System.Models;
using System.IO;

namespace Resturant_System.Controllers
{
    public class MealController : Controller
    {
        private MealDBContext db = new MealDBContext();
        private Meal_CategoryDBContext db_catgory = new Meal_CategoryDBContext();

        //
        // GET: /Meal/

        public ActionResult Index()
        {
            meal_categorys_intoView();
            return View(db.Meal.ToList());
        }
        //
        // GET: /Meal/Details/5

        public ActionResult Details(int id = 0)
        {
            Meal meal = db.Meal.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            meal_categorys_intoView();
            return View(meal);
        }

        //
        // GET: /Meal/Create

        public ActionResult Create(int id = 0)
        {

            meal_categorys_intoView();
            return View();
        }

        //
        // POST: /Meal/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Meal.Add(meal);
                db.SaveChanges(); 
                // add image for this 
                var q = (from a in db.Meal select a).ToList().Last();
                UploadObject upd = new UploadObject("Meal", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
            }

            return View(meal);
        }
        //......................ADD Image..................................
        public ActionResult upload_image(UploadObject up)
        {
            if (Request != null)
            {
                up.file = Request.Files["file"];
            }
            return View(up);
        }
        [HttpParamAction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Img_Upload(UploadObject up)
        {
            if (Request != null)
            {
                up.file = Request.Files["file"];

                if (up.file != null)
                {
                    string pic = System.IO.Path.GetFileName(up.file.FileName);
                    // depends of the type
                    string dirc_path = Server.MapPath("/Content/upload_images/" + up.nameOfTable);
                    bool isExists = System.IO.Directory.Exists(dirc_path);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory((dirc_path));
                    string path = System.IO.Path.Combine(dirc_path, pic);
                    // file is uploaded
                    up.file.SaveAs(path);
                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        up.file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    ///////////// Querey By name of table ///////////////
                    var q = from a in db.Meal where a.Id == up.idOfElement select a;
                    var lst = q.ToList();
                    var category = lst.Last();
                    category.Image = "/Content/upload_images/" + up.nameOfTable + "/" + pic;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        [HttpParamAction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skip_Img_Upload(UploadObject up)
        {
            if (Request != null)
            {
                up.file = Request.Files["file"];
                if (up.file != null)
                {
                    return RedirectToAction("Index");
                }
            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index");
        }
        //........................................................
        // // Get List Of Meals_Category into View category 
        void meal_categorys_intoView() {
            // To add dropdown list of Meal_Category
            var myCatgs = from m in db_catgory.Meal_Category select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myCatgs)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.CategoryID = lst_Select_Item;
        }
        // GET: /Meal/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Meal meal = db.Meal.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            // To add dropdown list of Meal_Category
            meal_categorys_intoView();

            return View(meal);
        }

        //
        // POST: /Meal/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                // add image for this 
                var q = meal; UploadObject upd = new UploadObject("Meal", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
            }
            return View(meal);
        }

        //
        // GET: /Meal/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Meal meal = db.Meal.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            meal_categorys_intoView();
            return View(meal);
        }

        //
        // POST: /Meal/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.Meal.Find(id);
            db.Meal.Remove(meal);
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