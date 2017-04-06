using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace Resturant_System.Models
{
    [Table("Meal_Suggestion")]
    public class Meal_Suggestion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int Category_Id { get; set; }
        public Meal_Suggestion() { }
        public Meal_Suggestion(string name , int categryId) { 
            this.Name = name ;
            this.Category_Id = categryId;
        }


    }
    public class Meal_SuggestionDBContext : DbContext
    {
        public DbSet<Meal_Suggestion> Meal_Suggestion { get; set; }
    }
}