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
    public class UsersController : Controller
    {
        private UsersDBContext db = new UsersDBContext();
        private User_RoleDBContext db_user_roles = new User_RoleDBContext();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            roles_of_users_intoView();
            return View(db.users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            roles_of_users_intoView();
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                if (users.Role_id == 0) return View(users);
                db.SaveChanges();
                // add image for this 
                var q = (from a in db.users select a).ToList().Last();
                UploadObject upd = new UploadObject("Users", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
                
            }

            return View(users);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            // To add dropdown list of UserRoles
            roles_of_users_intoView();
            return View(users);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                if (users.Role_id == 0) return View(users);
                db.SaveChanges(); 
                // add image for this 
                var q = users;
                UploadObject upd = new UploadObject("Users", q.Id);
                return RedirectToAction("upload_image", upd);
                //return RedirectToAction("Index");
            }
            return View(users);
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
                    var q = from a in db.users where a.Id == up.idOfElement select a;
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
        // // Get List Of roles_of_users into View roles_of_users_intoView 
        void roles_of_users_intoView()
        {
            // To add dropdown list of Meal_Category
            var myRoles = from m in db_user_roles.user_role select m; 
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myRoles)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.Role_id = lst_Select_Item;
        }
        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.users.Find(id);
            db.users.Remove(users);
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