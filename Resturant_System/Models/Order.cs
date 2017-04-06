using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Resturant_System.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        [Display(Name = "Meal")]
        public int Meal_Id { get; set; }
        [Display(Name = "Invoice")]
        public int Invoice_Id { get; set; }
        [Display(Name = "Additions")]
        public string Extra { get; set; }

        public Order() { }
        public Order(bool active, int mealId, int invoiceId)
        {
            this.Active = active;
            this.Meal_Id = mealId;
            this.Invoice_Id = invoiceId;
        }
        public Order(bool active , int mealId , int invoiceId , string extra) {
            this.Active = active;
            this.Meal_Id = mealId ;
            this.Invoice_Id = invoiceId ; 
            this.Extra = extra; 
        }
        


    }

       public class OrderDBContext : DbContext
    {
           public DbSet<Order> Order { get; set; }
    }
}