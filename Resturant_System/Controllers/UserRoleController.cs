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
    public class UserRoleController : Controller
    {
        private User_RoleDBContext db = new User_RoleDBContext();

        //
        // GET: /UserRole/

        public ActionResult Index()
        {
            return View(db.user_role.ToList());
        }

        //
        // GET: /UserRole/Details/5

        public ActionResult Details(int id = 0)
        {
            User_Role user_role = db.user_role.Find(id);
            if (user_role == null)
            {
                return HttpNotFound();
            }
            return View(user_role);
        }

        //
        // GET: /UserRole/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserRole/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User_Role user_role)
        {
            if (ModelState.IsValid)
            {
                db.user_role.Add(user_role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_role);
        }

        //
        // GET: /UserRole/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User_Role user_role = db.user_role.Find(id);
            if (user_role == null)
            {
                return HttpNotFound();
            }
            return View(user_role);
        }

        //
        // POST: /UserRole/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User_Role user_role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_role);
        }

        //
        // GET: /UserRole/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User_Role user_role = db.user_role.Find(id);
            if (user_role == null)
            {
                return HttpNotFound();
            }
            return View(user_role);
        }

        //
        // POST: /UserRole/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Role user_role = db.user_role.Find(id);
            db.user_role.Remove(user_role);
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