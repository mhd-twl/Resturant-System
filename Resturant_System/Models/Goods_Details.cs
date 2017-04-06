using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Goods_Details")]
    public class Goods_Details
    {
        [Key]
        public int Id { get; set; }
        public double Quantity { get; set; }
        public int Price { get; set; }
        public DateTime Insertion_Date { get; set; }
        public int Goods_Id { get; set; }
        public string Extra { get; set; }

        public Goods_Details() { }
        public Goods_Details(double quantity, int price, DateTime insert_date, int goodsId)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.Insertion_Date = insert_date;
            this.Goods_Id = goodsId;
        }
        public Goods_Details(double quantity, int price, DateTime insert_date, int goodsId, string extra)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.Insertion_Date = insert_date;
            this.Goods_Id = goodsId;
            this.Extra = extra;
        }


    }
    public class Goods_DetailsDBContext : DbContext
    {
        public DbSet<Goods_Details> Goods_Details { get; set; }
    }
}