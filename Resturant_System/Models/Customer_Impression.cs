using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Customer_Impression")]
    public class Customer_Impression
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [Display(Name = "Invoice")]
        public int Invoice_Id { get; set; }
    }
    public class Customer_ImpressionDBContext : DbContext
    {
        public DbSet<Customer_Impression> Customer_Impressions { get; set; }
    }
}