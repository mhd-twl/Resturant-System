using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    public class InvoiceView
    {
        [Key]
        public int Invoice_Id { get; set; }
        [Display(Name = "Table #")]
        public int Invoice_TableID { get; set; }
        [Display(Name = "Opened Date")]
        public DateTime Invoice_Opendate { get; set; }
        [Display(Name = "Status")]
        public string Invoice_Status { get; set; }
        [Display(Name = "Total Cost")]
        public decimal Total_Cost { get; set; }
        public List<Meal> Meals_List { get; set; }

        [Display(Name = "Additions")]
        public string Extra { get; set; }


        public InvoiceView() { List<Meal> Meals_List = new List<Meal>(); }
        public InvoiceView(List<Meal> meal_lst, decimal total_cost, Invoice invoice)
        {
            //List<Meal> Meals_List = new List<Meal>();
        
        }
        public InvoiceView(List<Meal> meal_lst, decimal total_cost, int invoiceId,
            int invoiceTableId, DateTime invoiceOpendate, string invoiceStatus)
        {
            List<Meal> Meals_List = new List<Meal>();
            this.Meals_List = meal_lst;
            this.Total_Cost = total_cost;

            this.Invoice_Id =  invoiceId;
            this.Invoice_TableID = invoiceTableId;
            this.Invoice_Status = invoiceStatus;
            this.Invoice_Opendate = invoiceOpendate;
        }
        public InvoiceView( List<Meal> meal_lst, decimal total_cost,
            int invoiceId, int invoiceTableId, DateTime invoiceOpendate, string invoiceStatus, string extra)
        {
            List<Meal> Meals_List = new List<Meal>();
            this.Meals_List = meal_lst;
            this.Total_Cost = total_cost;

            this.Invoice_Id = invoiceId;
            this.Invoice_TableID = invoiceTableId;
            this.Invoice_Status = invoiceStatus;
            this.Invoice_Opendate = invoiceOpendate;

            this.Extra = extra; 
        }
    }
    public class InvoiceViewDBContext : DbContext
    {
        public DbSet<InvoiceView> InvoiceView { get; set; }
    }
}