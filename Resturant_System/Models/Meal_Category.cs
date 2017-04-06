using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Meal_Category")]
    public class Meal_Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Meal_Category() { }
        public Meal_Category(string name , string img) {
            this.Name = name;
            this.Image = img; 
        }
    }
    public class Meal_CategoryDBContext : DbContext
    {
        public DbSet<Meal_Category> Meal_Category { get; set; }
    }
}