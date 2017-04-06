using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
     [Table("Goods")]
    public class Goods
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime Modified_Date { get; set; }
        [Display(Name = "Total Quantity")]
        public double Total_Quantity { get; set; }
        public int CategoryID { get; set; }
        public string Extra { get; set; } // Quantity_Total

        public Goods() { }
        public Goods(string name, string image, string unit, decimal price, DateTime modified_date, double total_quantity, int catgId)
        {
            this.Name = name;
            this.Image = image;
            this.Unit = unit;
            this.Price = price;
            this.Modified_Date = modified_date;
            this.Total_Quantity = total_quantity;
            this.CategoryID = catgId;
        }
        public Goods(string name, string image, string unit, decimal price, DateTime modified_date, double total_quantity, int catgId , string extra)
        {
            this.Name = name;
            this.Image = image;
            this.Unit = unit;
            this.Price = price;
            this.Modified_Date = modified_date;
            this.Total_Quantity = total_quantity;
            this.CategoryID = catgId;
            this.Extra = extra;
        }
    }
     public class GoodsDBContext : DbContext
     {
         public DbSet<Goods> Goods { get; set; }
     }
}