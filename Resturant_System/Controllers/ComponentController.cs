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
    public class ComponentController : Controller
    {
        private ComponentDBContext db = new ComponentDBContext();

        //
        // GET: /Component/

        public ActionResult Index()
        {
            return View(db.Components.ToList());
        }

        //
        // GET: /Component/Details/5

        public ActionResult Details(int id = 0)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        //
        // GET: /Component/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Component/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Component component)
        {
            if (ModelState.IsValid)
            {
                db.Components.Add(component);
                db.SaveChanges();
                // add image for this 
                var q = (from a in db.Components select a).ToList().Last();
                UploadObject upd = new UploadObject("Components", q.Id);
                return RedirectToAction("upload_image", upd);

                //return RedirectToAction("Index");
            }

            return View(component);
        }

        //
        // GET: /Component/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        //
        // POST: /Component/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Component component)
        {
            if (ModelState.IsValid)
            {
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                // add image for this 
                var q = component; UploadObject upd = new UploadObject("Component", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
            }
            return View(component);
        }

        //
        // GET: /Component/Delete/5
        //......................ADD Image..................................
        public ActionResult upload_image(UploadObject up)
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
                    var q = from a in db.Components where a.Id == up.idOfElement select a;
                    var lst = q.ToList();
                    var component = lst.Last();
                    component.Image = "/Content/upload_images/" + up.nameOfTable + "/" + pic;
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

        public ActionResult Delete(int id = 0)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        //
        // POST: /Component/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Components.Find(id);
            db.Components.Remove(component);
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