using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resturant_System.Models;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;
using System.Web.UI.WebControls;


namespace Resturant_System.Controllers
{
    public class MealCategoryController : Controller
    {
        private Meal_CategoryDBContext db = new Meal_CategoryDBContext();

        //
        // GET: /MealCategory/

        public ActionResult Index()
        {
            return View(db.Meal_Category.ToList());
        }

        //
        // GET: /MealCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            Meal_Category meal_category = db.Meal_Category.Find(id);
            if (meal_category == null)
            {
                return HttpNotFound();
            }
            return View(meal_category);
        }

        //
        // GET: /MealCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MealCategory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meal_Category meal_category)
        {
            if (ModelState.IsValid)
            {
                db.Meal_Category.Add(meal_category);
                db.SaveChanges();
                // add image for this 
                var q = (from a in db.Meal_Category  select a).ToList().Last();
                UploadObject upd = new UploadObject("Meal_Category", q.Id );
                return RedirectToAction("upload_image", upd);
             }

            return View(meal_category);
        }
        //......................ADD Image..................................
        public ActionResult upload_image(UploadObject up )
        {
            if (Request != null)
            {
                up.file = Request.Files["file"];
                if (up.file != null)
                {
                }
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
                    string dirc_path = Server.MapPath("/Content/upload_images/" + up.nameOfTable );
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
                    var q = from a in db.Meal_Category where a.Id == up.idOfElement  select a;
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
        //
        // GET: /MealCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Meal_Category meal_category = db.Meal_Category.Find(id);
            if (meal_category == null)
            {
                return HttpNotFound();
            }
            return View(meal_category);
        }

        //
        // POST: /MealCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Meal_Category meal_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal_category).State = EntityState.Modified;
                db.SaveChanges();
                // add image for this 
                var q = meal_category; UploadObject upd = new UploadObject("Meal_Category", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
            }
            return View(meal_category);
        }

        //
        // GET: /MealCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Meal_Category meal_category = db.Meal_Category.Find(id);
            if (meal_category == null)
            {
                return HttpNotFound();
            }
            return View(meal_category);
        }

        //
        // POST: /MealCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal_Category meal_category = db.Meal_Category.Find(id);
            db.Meal_Category.Remove(meal_category);
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