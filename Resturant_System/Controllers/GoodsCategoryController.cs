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
    public class GoodsCategoryController : Controller
    {
        private Goods_CategoryDBContext db = new Goods_CategoryDBContext();

        //
        // GET: /GoodsCategory/

        public ActionResult Index()
        {
            return View(db.Goods_Category.ToList());
        }

        //
        // GET: /GoodsCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            Goods_Category goods_category = db.Goods_Category.Find(id);
            if (goods_category == null)
            {
                return HttpNotFound();
            }
            return View(goods_category);
        }

        //
        // GET: /GoodsCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GoodsCategory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Goods_Category goods_category)
        {
            if (ModelState.IsValid)
            {
                db.Goods_Category.Add(goods_category);
                db.SaveChanges();
                // add image for this
                var q = (from a in db.Goods_Category select a).ToList().Last();
                UploadObject upd = new UploadObject("Good_Category", q.Id);
                return RedirectToAction("upload_image", upd);
            }

            return View(goods_category);
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
                    var q = from a in db.Goods_Category where a.Id == up.idOfElement select a;
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

        //
        // GET: /GoodsCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Goods_Category goods_category = db.Goods_Category.Find(id);
            if (goods_category == null)
            {
                return HttpNotFound();
            }
            return View(goods_category);
        }

        //
        // POST: /GoodsCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Goods_Category goods_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goods_category).State = EntityState.Modified;
                db.SaveChanges();
                // add image for this
                var q = goods_category; UploadObject upd = new UploadObject("Good_Category", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
            }
            return View(goods_category);
        }

        //
        // GET: /GoodsCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Goods_Category goods_category = db.Goods_Category.Find(id);
            if (goods_category == null)
            {
                return HttpNotFound();
            }
            return View(goods_category);
        }

        //
        // POST: /GoodsCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goods_Category goods_category = db.Goods_Category.Find(id);
            db.Goods_Category.Remove(goods_category);
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