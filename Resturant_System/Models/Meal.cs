using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Meal")]
    public class Meal
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Recipe  { get; set; }
        public string Available { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Meal() { }
        public Meal(string name, string image, string availbe, decimal price, int catgId)
        {
            this.Name = name;
            this.Image = image;
            this.Available = availbe;
            this.Price = price;
            this.CategoryId = catgId;
        }
        public Meal(string name , string image , string recipe , string availbe , decimal price ,int catgId ) {
            this.Name = name;
            this.Image = image;
            this.Recipe = recipe;
            this.Available = availbe;
            this.Price = price;
            this.CategoryId = catgId;
        }


    }

    public class MealDBContext : DbContext
    {
        public DbSet<Meal> Meal { get; set; }
    }
}