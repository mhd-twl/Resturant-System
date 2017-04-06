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
    public class ReservationController : Controller
    {
        private Reserved_TableDBContext db = new Reserved_TableDBContext();
        private TableDBContext db_table = new TableDBContext();
        //
        // GET: /Reservation/

        public ActionResult Index()
        {
            table_intoView();
            return View(db.Reserved_Table.ToList());
        }

        //
        // GET: /Reservation/Details/5

        public ActionResult Details(int id = 0)
        {
            Reserved_Table reserved_table = db.Reserved_Table.Find(id);
            table_intoView();
            tableUnResStts_intoView();
            if (reserved_table == null)
            {
                return HttpNotFound();
            }
            return View(reserved_table);
        }

        //
        // GET: /Reservation/Create

        public ActionResult Create()
        {
            table_intoView();
            tableUnResStts_intoView();
            return View();
        }

        //
        // POST: /Reservation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserved_Table reserved_table)
        {
            if (ModelState.IsValid)
            {
                db.Reserved_Table.Add(reserved_table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reserved_table);
        }

        //
        // GET: /Reservation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reserved_Table reserved_table = db.Reserved_Table.Find(id);
            table_intoView();
            if (reserved_table == null)
            {
                return HttpNotFound();
            }
            return View(reserved_table);
        }

        //
        // POST: /Reservation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reserved_Table reserved_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserved_table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reserved_table);
        }

        //
        // GET: /Reservation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Reserved_Table reserved_table = db.Reserved_Table.Find(id);
            table_intoView();
            if (reserved_table == null)
            {
                return HttpNotFound();
            }
            return View(reserved_table);
        }

        //
        // POST: /Reservation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserved_Table reserved_table = db.Reserved_Table.Find(id);
            db.Reserved_Table.Remove(reserved_table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        void table_intoView()
        {
            // To add dropdown list of tables
            var myTable = from m in db_table.Table select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myTable)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Unit_Name, false));
            ViewBag.Tables = lst_Select_Item;
        }
        void tableResStts_intoView()
        {
            // To add dropdown list of tables
            var myTableRes = from m in db.Reserved_Table where m.Table_Status == true orderby m.Table_Id select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myTableRes)
            {
                Select_Item slt = new Select_Item(item.Table_Id, item.Extra, item.Table_Status);
                lst_Select_Item.Add(slt);
            }
            ViewBag.TablesRes = lst_Select_Item;
        }
        void tableUnResStts_intoView()
        {
            // To add dropdown list of tables
            var myTableUnRes = from m in db.Reserved_Table where m.Table_Status == true orderby m.Table_Id select m;
            var myTable = from t in db_table.Table select t;

            List<int> ls = new List<int>();
            foreach (var item in myTableUnRes)
            {
                ls.Add(item.Table_Id);
            }
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var tbl in myTable)
            {
                if (!ls.Contains(tbl.Id))
                {
                    Select_Item slt = new Select_Item(tbl.Id, tbl.Unit_Name, false);
                    lst_Select_Item.Add(slt);
                }
            }
               
            ViewBag.TablesUnRes = lst_Select_Item;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}