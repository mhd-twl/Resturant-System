using Resturant_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Resturant_System.Controllers
{
    public class HomeController : Controller
    {
        private TableDBContext dbTables = new TableDBContext();
        private UsersDBContext dbUsers = new UsersDBContext();
        private User_RoleDBContext dbUsersRole = new User_RoleDBContext();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            table_intoView();
            return View();
        }

        public ActionResult CheckLogin(List<string> val)
        {
            int type = val.Count;
            MealCategoryController mc = new MealCategoryController();
            if (type == 1)
            {
                string s = val.First().Trim();
                Table t = (from a in dbTables.Table where a.Unit_Name == s select a ).ToList().First();
                if (t != null)
                {
                    Session["table"] = val.First();
                    return Json( 1 , JsonRequestBehavior.AllowGet);
                }
            }
            else 
            {
                string s1 = val.First().Trim();
                string s2 = val.Last().Trim();
                 Users usr = (from a in dbUsers.users where a.Username == s1 & a.Password == s2 select a).ToList().First();
                 if (usr != null)
                 {
                     if (usr.Role_id == 2)
                     {
                         Session["user"] = usr.Name; // val.First();
                         //return RedirectToAction(2 , "InvoiceView"); // 2 user
                         return Json(2, JsonRequestBehavior.AllowGet);
                     }
                     else if (usr.Role_id == 1)
                     {/// add to more  than chef
                         Session["admin"] = usr.Name; // val.First();
                         //return RedirectToAction(3, "Home");    // 3 admin
                         return Json(3, JsonRequestBehavior.AllowGet);
                     }
                 }
                 else
                 {
                     return Json(-1, JsonRequestBehavior.AllowGet);
                 }
            }
             return View();
             
                //return RedirectToAction("Index", "Home");
        }
        void table_intoView()
        {
            // To add dropdown list of tables
            var myTable = from m in dbTables.Table select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myTable)
            {
                // if (  Session["table"] != "yes")
                lst_Select_Item.Add(new Select_Item(item.Id, item.Unit_Name, false));
            }
            ViewBag.Tables = lst_Select_Item;
        }
     
    }
}
