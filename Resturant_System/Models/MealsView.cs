using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    public class MealsView
    {
        public Meal_Category Meals_Category { get; set; }
        public List<Meal> Meals_List { get; set; }

        public MealsView() {
            Meal_Category mealsCategory = new Meal_Category();
            this.Meals_Category = mealsCategory;
            List<Meal> mealsList = new List<Meal>();
        }
        public MealsView(Meal_Category mealsCategory, List<Meal> mealsList) 
        {
            //mealsCategory = new Meal_Category();
            this.Meals_Category = mealsCategory;
            //mealsList = new List<Meal>();
            this.Meals_List = mealsList;
        }

    }
    public class MealsViewDBContext : DbContext
    {
        public DbSet<MealsView> InvoiceView { get; set; }
    }
}