using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Meal_Content")]
    public class Meal_Content
    {
        [Key]
        public int Id { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        [Display(Name = " Meal kind")]
        public int Meal_Id { get; set; }
        [Display(Name = "Goods kind")]
        public int Goods_Id { get; set; }
        public string Extra { get; set; }
        

        public Meal_Content() { }
        public Meal_Content(string quantity,int mealId)
        {
            this.Quantity = quantity;
            this.Meal_Id = mealId;
        }
        public Meal_Content(string quantity, string unit, int mealId)
        {
            this.Quantity = quantity;
            this.Unit = unit;
            this.Meal_Id = mealId;
        }
        public Meal_Content(string quantity,  int mealId, int goodsId, string extra)
        {
            this.Quantity = quantity;
            this.Meal_Id = mealId;
            this.Goods_Id = goodsId;
            this.Extra = extra;
        }
        public Meal_Content(string quantity, string unit, int mealId,  string extra)
        {
            this.Quantity = quantity;
            this.Unit = unit;
            this.Meal_Id = mealId;
            this.Extra = extra;
        }
        public Meal_Content(string quantity, string unit, int mealId, int goodsId)
        {
            this.Quantity = quantity;
            this.Unit = unit;
            this.Meal_Id = mealId;
            this.Goods_Id = goodsId;
        }
        public Meal_Content(string quantity , string unit ,int  mealId ,int  goodsId ,string extra  ) 
        {
            this.Quantity = quantity;
            this.Unit = unit;
            this.Meal_Id = mealId;
            this.Goods_Id = goodsId;
            this.Extra = extra ; 
        }
    }
    public class Meal_ContentDBContext : DbContext
    {
        public DbSet<Meal_Content> Meal_Content { get; set; }
    }
}