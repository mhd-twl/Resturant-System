using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Resturant_System.Models
{
    public class Goods_Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Goods_Category() { }
        public Goods_Category(string name, string img)
        {
            this.Name = name;
            this.Image = img; 
        }
    }
    public class Goods_CategoryDBContext : DbContext
    {
        public DbSet<Goods_Category> Goods_Category { get; set; }

        public DbSet<Goods_Details> Goods_Details { get; set; }
    }
}