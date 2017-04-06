using Resturant_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resturant_System.Controllers
{
    public class InvoiceViewController : Controller
    {
        private InvoiceViewDBContext db = new InvoiceViewDBContext();
        private List<InvoiceView> db_list = new List<InvoiceView>();
        // GET: /InvoiceView/
        // new 
        private Invoice_StatusDBContext dbInvocStst = new Invoice_StatusDBContext();
        private InvoiceDBContext dbInvoices = new InvoiceDBContext();
        private OrderDBContext dbOrders = new OrderDBContext();
        private TableDBContext dbTables = new TableDBContext();
        private MealDBContext dbMeals = new MealDBContext();


        private List<InvoiceView> fill_this_db()
        {
            List<InvoiceView> lst = new List<InvoiceView>();
            var invoiceCollection = from a in dbInvoices.Invoice select a;
            var ordersCollection = from a in dbOrders.Order select a;

            foreach (var invc in invoiceCollection)
            {
                List<Meal> mealLst = new List<Meal>();
                decimal total_cost = 0;

                string invoiceStatsName = (from a in dbInvocStst.Invoice_Status where (a.Id == invc.Invoice_Status) select a).First().Name;
                foreach (var ords in ordersCollection)
                {
                    if (ords.Invoice_Id == invc.Id && ords.Active)
                    {
                        var mealCollection = from a in dbMeals.Meal where a.Id == ords.Meal_Id select a;
                        foreach (var items in mealCollection)
                        {
                            mealLst.Add(items);
                            total_cost += items.Price;
                        }
                    }
                }
                InvoiceView invTmp = new InvoiceView(mealLst, total_cost, invc.Id,
                    invc.Table_Id, invc.Open_Date, invoiceStatsName , invc.Extra);
                lst.Add(invTmp);
            }
            this.db_list = lst;
            return lst;
        }
        void invoice_status_intoView()
        {
            // To add dropdown list of Meal_Category
            var myInvSts = from m in dbInvocStst.Invoice_Status select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myInvSts)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Name, false));
            ViewBag.InvSts = lst_Select_Item;
        }
        private void table_intoView()
        {
            var myTable = from m in dbTables.Table select m;
            ViewBag.message = "";
            List<Select_Item> lst_Select_Item = new List<Select_Item>();
            foreach (var item in myTable)
                lst_Select_Item.Add(new Select_Item(item.Id, item.Unit_Name, false));
            ViewBag.Tables = lst_Select_Item;
        }
        public ActionResult Index()
        {
            this.db_list = fill_this_db();
            table_intoView();
            invoice_status_intoView();
            return View(db_list);
        }
        public ActionResult setListCust(List<string> itmsCmprLst)
        {

            if (itmsCmprLst.Count != 0)
            {
                List<List<string>> ls2 = new List<List<string>>();
                foreach (var item in itmsCmprLst)
                {
                    string s = item.Replace(@"^", ",");
                    string[] ss = s.Split(',');
                    List<string> ls = new List<string>();
                    foreach (var i in ss)
                    {
                        ls.Add(i);
                    }
                    ls2.Add(ls);
                    ls = new List<string>();
                }
                Session["user_meals"] = ls2;
                string table_unt = Session["table"] as string;
                Create_New(ls2, table_unt);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet); //myItems  "Hii"
        }
        public void Create_New(List<List<string>> List, string table_unt)
        {
            int invoice_stat = 1;  /// constents 
            string tbl_id = table_unt;
            string note =  List.Last()[1];
            List.RemoveAt(List.Count - 1);
            List<List<string>> ls_ms = List;
            List<Meal> meals = new List<Meal>();
            int t = (from a in dbTables.Table where a.Unit_Name == tbl_id select a).ToList().First().Id;
            Invoice invoice = new Invoice(DateTime.Now, DateTime.MaxValue, invoice_stat, t, note);
            // create
            InvoiceController ic = new InvoiceController();
            OrdersController oc = new OrdersController();
            int id_inv = ic.CreateInvoiceRetId(invoice);
            // adding
            decimal ttl_cst = 0;
            foreach (List<string> item in ls_ms)
            {
                int id = int.Parse(item.First().Trim());
                Meal m = (from a in dbMeals.Meal where a.Id == id select a).ToList().First();
                int numbOf = int.Parse(item.Last().Trim());
                for (int i = 0; i < numbOf; i++)
                {
                    meals.Add(m);
                    ttl_cst += m.Price;
                    Order order = new Order(true, m.Id, invoice.Id);
                    int id_ord = oc.CreateOrderRetId(order); //this 's id not important    
                }
            }
            //----------
            //InvoiceView m = new InvoiceView(meals,  ttl_cst, invoice);
            //--------
        }
        public bool EditInvoice(List<int> invoc)
        {
            int invSts = invoc[0]; int invoiceId = invoc[1];
            Invoice invoice = (from a in dbInvoices.Invoice where a.Id == invoiceId select a).ToList().First();
            invoice.Invoice_Status = invSts;
            if (ModelState.IsValid)
            {
                dbInvoices.Entry(invoice).State = EntityState.Modified;
                dbInvoices.SaveChanges();
                /// TODO resend to meal view
                MealsViewController mv = new MealsViewController();
                //mv. = "";

                //Index();
                return true;
            }
            return false;
        }

    }
}


//public ActionResult Edit(int id= 0)
//        {
//            this.db_list = fill_this_db();
//            table_intoView();
//            invoice_status_intoView();
//            Invoice invoice = (from a in dbInvoices.Invoice where a.Id == id select a).First();
//            return View(invoice);
//        }
//        //
//        // POST: /InvoiceView/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(Invoice invoice)
//        {
//            if (ModelState.IsValid)
//            {
//                dbInvoices.Entry(invoice).State = EntityState.Modified;
//                dbInvoices.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View();
//      }



////
//// GET: /InvoiceView/Details/5

//public ActionResult Details(int id = 0 )
//{
//    return View();
//}

////
//// GET: /InvoiceView/Create

//public ActionResult Create()
//{
//    return View();
//}

////
//// POST: /InvoiceView/Create

//[HttpPost]
//public ActionResult Create(InvoiceView invoiceView)
//{
//    try
//    {
//        // TODO: Add insert logic here

//        return RedirectToAction("Index");
//    }
//    catch
//    {
//        return View();
//    }
//}

//
// GET: /InvoiceView/Edit/5
//
// GET: /InvoiceView/Delete/5

//public ActionResult Delete(int id)
//{
//    return View();
//}

////
//// POST: /InvoiceView/Delete/5

//[HttpPost]
//public ActionResult Delete(int id, FormCollection collection)
//{
//    try
//    {
//        // TODO: Add delete logic here

//        return RedirectToAction("Index");
//    }
//    catch
//    {
//        return View();
//    }
//}